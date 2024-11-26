using Microsoft.EntityFrameworkCore;
using ServiceProvidersDirectory.Data;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("CardinalDbConnection") ?? throw new InvalidOperationException("Connection string 'CardinalDbConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAuthentication("AuthSchema").AddCookie("AuthSchema", opt =>
{
    opt.LoginPath = "/Accounts/LoginCardinal";
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
}
);

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 401)
    {
        context.HttpContext.Response.Redirect("/Home/AccessDenied");
    }
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//await app.MigrateDbAsync();

app.Run();
