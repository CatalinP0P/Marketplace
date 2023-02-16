using Microsoft.AspNetCore.Mvc;

using Marketplace.Models;
using Marketplace.Data;
using Marketplace.Interfaces;

namespace Marketplace.Controllers
{
    public class OferteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OferteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string categorie = "", string county="", string city="", string q = "", int minPrice=0, int maxPrice=1000000000 )
        {
            var viewModel = new OferteViewModel();
            viewModel.Anunturi = new List<Anunt>();
            viewModel.Categories = new List<string>();

            foreach ( var anunt in _context.Anunturi.ToList() )
            {
                if (
                anunt.Category.Contains(categorie) &&
                anunt.County.Contains(county) &&
                anunt.Title.Contains(q) &&
                anunt.City.Contains(city) &&
                anunt.Price >= minPrice && anunt.Price <= maxPrice )
                {
                    viewModel.Anunturi.Add(anunt);
                }
            }

            foreach ( var categ in _context.Categories )
            {
                viewModel.Categories.Add(categ.Name);
            }

            viewModel.Filters = new FilterModel()
            {
                Category = categorie,
                County = county,
                City = city,
                q = q,
                minPrice = minPrice,
                maxPrice = maxPrice,
            };
            

            return View("Index", viewModel);

        }
    }
}