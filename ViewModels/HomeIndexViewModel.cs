using Marketplace.Interfaces;
using Marketplace.Models;

namespace Marketplace.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Anunt>? Anunturi { get; set; }
        public List<Categorie> Categories { get; set; }
    }

}