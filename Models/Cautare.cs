namespace Marketplace.Models
{
    public class Cautare
    {
        public int Id { get; set; }
        public string? userId { get; set; }
        public string? Text { get; set; }
        public string? Category { get; set; }
        public string? City { get; set; }
        public string? County { get; set; }
        public int? minPrice { get; set; }
        public int? maxPrice { get; set; }
    }
}