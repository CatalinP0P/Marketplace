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

        public IActionResult Index(string categorie = "", string q = "")
        {
            var viewModel = new OferteViewModel();

            if (categorie == "" && q == "")
            {
                viewModel.Anunturi = _context.Anunturi.ToList();
            }
            else if ( categorie != "" && q!= "" )
            {
                viewModel.Anunturi = new List<Anunt>();

                foreach( var anunt in _context.Anunturi )
                {
                    if ( anunt.Category == categorie ) 
                        if ( anunt.Title.ToLower().Contains(q.ToLower()) )
                            viewModel.Anunturi.Add(anunt);
                } 
            }
            else if ( categorie != "" )
            {
                viewModel.Anunturi = new List<Anunt>();

                foreach( var anunt in _context.Anunturi )
                {
                    if ( anunt.Category == categorie ) 
                    {
                        viewModel.Anunturi.Add(anunt);
                    }
                } 
            }
            else if ( q != "" )
            {
                viewModel.Anunturi = new List<Anunt>();

                foreach( var anunt in _context.Anunturi )
                {
                    if ( anunt.Title.ToLower().Contains(q.ToLower()) )
                        viewModel.Anunturi.Add(anunt);
                } 
            }

            return View("Index", viewModel);

        }
    }
}