using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Marketplace.Models;
using Marketplace.Data;
using Marketplace.Interfaces;
using System.Security.Claims;

namespace Marketplace.Controllers
{
    [Authorize]
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FavouritesController(ApplicationDbContext context)
        {
            _context = context;
        }        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Anunturi()
        {
            var userId = "notfound";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                userId = claims.Value;
            }

            var list = new List<Anunt>();
            foreach ( var anuntFav in _context.Favourites.Where(m=>m.UserId == userId) )
            {
                var anunt = _context.Anunturi.FirstOrDefault(m=>m.Id == anuntFav.AnuntId);
                if ( anunt != null )
                    list.Add(anunt);
            }
            list.Reverse();

            return View("Anunturi", list);
        }

        public IActionResult Searches()
        {
            return View();
        }
    }
}