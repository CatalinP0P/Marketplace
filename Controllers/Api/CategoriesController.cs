using Microsoft.AspNetCore.Mvc;

using Marketplace.Data;
using Marketplace.Models;

namespace Marketplace.Controllers.Api
{  
    [ApiController]
    [Route("api/{controller}")]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<string> GetCategoriesName()
        {
            var categoriesList = _context.Categories.ToList();
            
            var categoriesName = new List<string>();

            foreach ( var cat in categoriesList )
            {
                categoriesName.Add(cat.Name);
            }

            return categoriesName;
        }

        [HttpGet]
        [Route("{name}")]
        public string GetCategorieImage( string name )
        {
            var categorieInDb = _context.Categories.Single(m=>m.Name == name);
            return categorieInDb.Image;
        }
    }

}