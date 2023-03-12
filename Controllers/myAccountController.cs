using Microsoft.AspNetCore.Mvc;

using Marketplace.Controllers;
using Marketplace.Data;
using Marketplace.Interfaces;
using Marketplace.Models;
using Marketplace.ViewModels;
using System.Security.Claims;

namespace Marketplace.Controllers
{
    public class myAccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string _userId;

        public myAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Anunturi()
        {   
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                _userId = claims.Value;
            }

            var anunturi = new List<Anunt>();
            foreach( var anunt in _context.Anunturi )
            {
                if ( anunt.AccountId == _userId )
                {
                    anunturi.Add(anunt);
                }
            }
            return View(anunturi);
        }
       
    }
}