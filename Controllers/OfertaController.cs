using Microsoft.AspNetCore.Mvc;

using Marketplace.Models;
using Marketplace.Data;
using Marketplace.Controllers;
using Marketplace.ViewModels;

namespace Marketplace.Controllers
{
    public class OfertaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OfertaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var anuntInDb = _context.Anunturi.SingleOrDefault(m=>m.Id == id);
            if ( anuntInDb == null )
                return RedirectToAction("Error404", "Error");
            
            return View("Index", anuntInDb);
        }
    }
}