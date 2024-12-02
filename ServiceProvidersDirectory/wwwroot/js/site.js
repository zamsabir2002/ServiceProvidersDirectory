

$(document).ready(function () {
    let selectedServices = []; // For selected services
    let hasFetchedInitialData = false;


    $("#service-container").on("click", function () {
        $("#service-search").focus();
        if (!hasFetchedInitialData) {
            fetchAllServices(); // Fetch initial services on first click
        } else {
            $("#service-dropdown").css("top", $(".search-container").outerHeight() ); 
            $("#service-dropdown").show(); // Show the dropdown if it's already populated

        }
    });

    // Fetch all services for the dropdown (initial load)
    function fetchAllServices() {
        $.ajax({
            url: `/Services/Search?query=`, // Empty query fetches all services
            method: 'GET',
            success: function (data) {
                renderDropdown(data);
                hasFetchedInitialData = true;
            },
            error: function () {
                $("#service-dropdown").html('<div class="dropdown-item text-muted">Error fetching services</div>');
                $("#service-dropdown").css("top", $(".search-container").outerHeight() ); 
                $("#service-dropdown").show();
            }
        });
    }

    // Fetch services dynamically as the user types
    $("#service-search").on("input", function () {
        const query = $(this).val().trim();

        // Do nothing if the query is empty
        if (!query) {
            fetchAllServices();
            return;
        }

        $.ajax({
            url: `/Services/Search?query=${query}`,
            method: 'GET',
            success: function (data) {
                renderDropdown(data);
            },
            error: function () {
                $("#service-dropdown").html('<div class="dropdown-item text-muted">Error fetching services</div>');
                $("#service-dropdown").css("top", $(".search-container").outerHeight() ); 
                $("#service-dropdown").show();
            }
        });
    });

    // Render dropdown items
    function renderDropdown(services) {
        if (services.length === 0) {
            $("#service-dropdown").css("top", $(".search-container").outerHeight());
            $("#service-dropdown").html('<div class="dropdown-item text-muted">No matches found</div>').show();
        } else {
            const dropdown = services.map(service =>
                `<a href="#" class="dropdown-item" data-id="${service.id}" data-name="${service.name}">
                    ${service.name}
                </a>`
            ).join("");
            $("#service-dropdown").css("top", $(".search-container").outerHeight());
            $("#service-dropdown").html(dropdown).show();
        }
    }

    // Handle service selection
    $("#service-dropdown").on("mousedown", ".dropdown-item", function (e) {
        e.preventDefault();
        const id = $(this).data("id");
        const name = $(this).data("name");
        addService(id, name);

        // Re-fetch all services to keep the dropdown open
        fetchAllServices();
    });

    // Add service to the selected list
    function addService(id, name) {
        if (!selectedServices.some(s => s.id === id)) {
            selectedServices.push({ id, name });

            // Append the tag
            $(".selected-services").append(`
                <span class="m-1 px-2 py-1 rounded-1 d-flex align-items-center gap-3" style="background-color: #007bff; color: white; ">
                    ${name}
                    <button type="button" class="btn btn-sm text-light remove-service p-0 border-0" data-id="${id}">
                        <i class="bi bi-x-circle-fill"></i>
                    </button>
                </span>
            `);
        }

        // Reset search input and keep the input focused
        $("#service-search").val('');
        $("#service-search").focus();
    }

    // Remove service from selected list
    $("#service-container").on("click", ".remove-service", function () {
        const id = $(this).data("id");
        selectedServices = selectedServices.filter(s => s.id !== id);
        $(this).parent().remove();
    });

    // Handle dropdown visibility
    $("#service-search").on("focus", function () {
        if (hasFetchedInitialData) {
            $("#service-dropdown").css("top", $(".search-container").outerHeight()); 
            $("#service-dropdown").show();
        }
    });

    $("#service-search").on("blur", function () {
        // Delay hiding dropdown to allow mouse click 
        setTimeout(() => $("#service-dropdown").hide(), 200);
    });

    // Escape key to close the dropdown
    $("#service-search").on("keydown", function (e) {
        const items = $("#service-dropdown .dropdown-item");
        let activeIndex = items.index($(".dropdown-item.active"));

        if (e.key === "Escape") {
            $("#service-dropdown").hide();
            return;
        }

        if (e.key === "ArrowDown") {
            e.preventDefault();
            activeIndex = (activeIndex + 1) % items.length;
            scrollToActiveItem(items, activeIndex);
        } else if (e.key === "ArrowUp") {
            e.preventDefault();
            activeIndex = (activeIndex - 1 + items.length) % items.length;
            scrollToActiveItem(items, activeIndex);
        } else if (e.key === "Enter") {
            e.preventDefault();
            if (activeIndex > -1) {
                const selectedItem = items.eq(activeIndex);
                const id = selectedItem.data("id");
                const name = selectedItem.data("name");
                addService(id, name);

                // refresh all services and keep dropdown open
                fetchAllServices();
            }
        }

        items.removeClass("active");
        if (activeIndex > -1) {
            items.eq(activeIndex).addClass("active");
        }
    });

    // Scroll dropdown to keep active item visible
    function scrollToActiveItem(items, activeIndex) {
        const dropdown = $("#service-dropdown");
        const activeItem = items.eq(activeIndex);
        const dropdownHeight = dropdown.height();
        const dropdownScrollTop = dropdown.scrollTop();
        const itemTop = activeItem.position().top + dropdownScrollTop;
        const itemBottom = itemTop + activeItem.outerHeight();

        if (itemBottom > dropdownScrollTop + dropdownHeight) {
            dropdown.scrollTop(itemBottom - dropdownHeight);
        } else if (itemTop < dropdownScrollTop) {
            dropdown.scrollTop(itemTop);
        }
    }

    // Hospital search button click
    $("#search-button").on("click", function () {
        const searchFilter = $("input[name='searchFilter']:checked").val();
        const searchQuery = $("#hospital-search").val();
        const selectedServiceIds = selectedServices.map(s => s.id);

        // Validate before sending
        if (selectedServiceIds.length === 0) {
            alert("Please select at least one service.");
            return;
        }

        // Perform search
        $.ajax({
            url: `/Search/SearchCenters`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                serviceIds: selectedServiceIds,
                filterType: searchFilter,
                query: searchQuery
            }),
            success: function (data) {
                if (data.redirectUrl) {
                    window.location.href = data.redirectUrl;
                } else {
                    console.log(data);
                    alert("No redirect URL provided.");
                }
            },
            error: function () {
                alert("Error performing search.");
            }
        });
    });


    // updating placeholder
    function updatePlaceholder() {
        const selectedFilter = $('input[name="searchFilter"]:checked').val();
        const searchInput = $("#hospital-search");

        if (selectedFilter === "postcode") {
            searchInput.attr("placeholder", "e.g. 12345");
        } else if (selectedFilter === "hospital") {
            searchInput.attr("placeholder", "e.g. Admiral Hospital");
        }
    }

    updatePlaceholder();

    // event listener for the radio buttons
    $('input[name="searchFilter"]').change(function () {
        updatePlaceholder();
    });
});

