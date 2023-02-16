using Marketplace.Models;

namespace Marketplace.Models
{
    public class OferteViewModel
    {
        public List<Anunt> Anunturi { get; set; }
        public List<string> Categories { get; set; }
        public FilterModel Filters { get;set; }
    }
}