using Microsoft.AspNetCore.Mvc;
using PagedList.Mvc;
using PagedList;

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

        public IActionResult Index(AutoProperties? auto, int pageIndex = 1, string categorie = "", string sortMethod = "random", string county="", string city="", string q = "", int minPrice=0, int maxPrice=1000000000 )
        {
            var viewModel = new OferteViewModel();
            var anunturiFiltrate = new List<Anunt>();
            viewModel.Categories = new List<string>();

            foreach( var anunt in _context.Anunturi ) 
            {
                if (
                anunt.Category.Contains(categorie) &&
                anunt.County.Contains(county) &&
                anunt.Title.ToLower().Contains(q.ToLower()) &&
                anunt.City.Contains(city) &&
                anunt.Price >= minPrice && anunt.Price <= maxPrice )
                {
                    anunturiFiltrate.Add(anunt);
                }
            }

            if ( sortMethod == SortingMethods.price_asc.ToString() )
            {
                for ( int i=0; i<anunturiFiltrate.Count; i++ )
                {
                    for ( int j=i+1; j<anunturiFiltrate.Count; j++ )
                    {
                        if ( anunturiFiltrate[i].Price > anunturiFiltrate[j].Price ) 
                        {
                            var aux = anunturiFiltrate[i];
                            anunturiFiltrate[i] = anunturiFiltrate[j];
                            anunturiFiltrate[j] = aux;
                        }
                    }
                }
            }
            
            if ( sortMethod == SortingMethods.price_desc.ToString() )
            {
                for ( int i=0; i<anunturiFiltrate.Count; i++ )
                {
                    for ( int j=i+1; j<anunturiFiltrate.Count; j++ )
                    {
                        if ( anunturiFiltrate[i].Price < anunturiFiltrate[j].Price ) 
                        {
                            var aux = anunturiFiltrate[i];
                            anunturiFiltrate[i] = anunturiFiltrate[j];
                            anunturiFiltrate[j] = aux;
                        }
                    }
                }
            }

            var AnunturiPagina = new List<Anunt>();
            double pageSize = 5;
            int index = (int)(pageIndex * pageSize - pageSize);
            int count = 0; 
            while ( count < pageSize && index < anunturiFiltrate.Count() )
            {
                AnunturiPagina.Add(anunturiFiltrate[index]);
                count++;
                index++;
            }


            viewModel.pageCount = (int)Math.Ceiling(anunturiFiltrate.Count()/pageSize);

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

            
            
            viewModel.Anunturi = AnunturiPagina;
            viewModel.pageIndex = pageIndex;
            viewModel.anunturiCount = anunturiFiltrate.Count();

            return View("Index", viewModel);
        }
    }
}