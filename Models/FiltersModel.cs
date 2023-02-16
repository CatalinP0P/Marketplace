namespace Marketplace.Models
{
    public class FilterModel
    {
        public string q { get; set; } = "";
        public string City { get; set; } = "";
        public string County { get; set; } ="";
        public string Category { get; set; } ="";
        public int minPrice{ get;set; }
        public int maxPrice { get;set; }
    }    
}