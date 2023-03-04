namespace Marketplace.Models
{
    public class Cautaure
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? Category { get; set; }
        public int? minPrice { get; set; }
        public int? maxPrice { get; set; }
    }
}