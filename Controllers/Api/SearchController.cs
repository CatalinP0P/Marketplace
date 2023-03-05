using Microsoft.AspNetCore.Mvc;

using Marketplace.Models;
using Marketplace.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Marketplace.Controllers.Api
{
    [Route("api/{controller}")]
    [Authorize]
    public class SearchController : Controller
    {       
        private readonly ApplicationDbContext _context;
        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Cautare> GetFavouries()
        {
            string _userId = "not found";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                _userId = claims.Value;
            }

            var list = _context.CautariFavorite.Where(m=>m.userId == _userId).ToList();
            list.Reverse();
            
            return list;
        }

        [HttpGet]
        [Route("{id}")]
        public Cautare GetById(int id)
        {
            var cautare = _context.CautariFavorite.SingleOrDefault(m=>m.Id == id);
            
            return cautare;
        }

        [Route("add")]
        [HttpPost]
        public void AddToFavourite(string text = "", string categorie = "", string county = "", string city = "", int minPrice = 0, int maxPrice = 1000000000)
        {   
            string _userId = "not found";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                _userId = claims.Value;
            }

            var cautare = new Cautare()
            {
                Text = text,
                Category = categorie,
                County = county,
                City = city,
                minPrice = minPrice,
                maxPrice = maxPrice,
                userId = _userId
            };

            _context.CautariFavorite.Add(cautare);
            _context.SaveChanges();
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public void RemoveFromFavourites(int id)
        {
            string _userId = "not found";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                _userId = claims.Value;
            }

            var cautare = _context.CautariFavorite.SingleOrDefault(m=>m.Id == id && m.userId == _userId );
            if ( cautare != null )
                _context.CautariFavorite.Remove(cautare);

            _context.SaveChanges();
        }

    }
}