using Marketplace.Models;
using PagedList;
using PagedList.Mvc;

namespace Marketplace.Models
{
    public class OferteViewModel
    {
        public List<Anunt> Anunturi { get; set; }
        public List<string> Categories { get; set; }
        public FilterModel Filters { get;set; }
        public int pageCount {get;set;}
        public int anunturiCount;
        public int pageIndex;
    }
}