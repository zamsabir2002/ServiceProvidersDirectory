namespace ServiceProvidersDirectory.Models
{
    public class SearchRequest
    {
        public required List<Guid> ServiceIds { get; set; }
        public required string FilterType { get; set; }
        public required string Query { get; set; }        

    }
}
