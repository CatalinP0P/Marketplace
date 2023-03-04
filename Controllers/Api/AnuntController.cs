using Microsoft.AspNetCore.Mvc;

using Marketplace.Data;
using Marketplace.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.Controllers.Api
{  
    [ApiController]
    [Route("api/{controller}")]
    public class AnuntController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnuntController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Anunt> GetAll()
        {
            return _context.Anunturi.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Anunt GetById( int id )
        {
            return _context.Anunturi.First(m=>m.Id == id);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            var anuntInDb = _context.Anunturi.Single(m=>m.Id == id);
            _context.Anunturi.Remove(anuntInDb);
            _context.SaveChanges();
        }

        [HttpPost]
        [Authorize]
        [Route("AddToFavourites/{id}")]
        public void AddToFavourite(int id)
        {
            string _userId = "";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                _userId = claims.Value;
            }

            foreach ( var item in _context.Favourites )
            {
                if ( item.AnuntId == id && item.UserId == _userId )
                    return;
            }
   
            var favouriteItem = new FavouriteItem
            {
                UserId = _userId,
                AnuntId = id,
            };

            _context.Favourites.Add(favouriteItem);
            _context.SaveChanges();
        }

        [Authorize]
        [HttpDelete]
        [Route("RemoveFromFavourites/{id}")]
        public void RemoveFromFavourites(int id)
        {
            string _userId = "";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                _userId = claims.Value;
            }

            foreach ( var item in _context.Favourites )
            {
                if ( item.AnuntId == id && item.UserId == _userId )
                {
                    _context.Favourites.Remove(item);
                    _context.SaveChanges();
                }
            }
        }
        
        [HttpGet]
        [Authorize]
        [Route("GetFavourites")]
        public List<int> GetFavourites()
        {
            var _userId = "";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                _userId = claims.Value;
            }

            var list = new List<int>();

            foreach ( var item in _context.Favourites )
                if ( item.UserId == _userId )
                    list.Add(item.AnuntId);

            return list;
        }

        [Authorize]
        [HttpGet]
        [Route("CheckFavourite/{id}")]
        public bool CheckIfFavourite(int id)
        {
            var _userId = "";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                _userId = claims.Value;
            }

            var item = _context.Favourites.SingleOrDefault( m=>m.AnuntId == id && m.UserId == _userId);

            if ( item == null )
                return false;
                
            return true;
        }
    }
}