using Microsoft.AspNetCore.Mvc;

using Marketplace.Data;
using Marketplace.Models;

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
        }
    }
}