using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using PagedList;
using PagedList.Mvc;

using Marketplace.Models;
using Marketplace.Data;
using Marketplace.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Marketplace.Interfaces;

namespace Marketplace.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IRoLocationService _locationService;

    public HomeController
        (
        ILogger<HomeController> logger,
        ApplicationDbContext context,
        IRoLocationService locationService
        )
    {
        _logger = logger;
        _context = context;
        _locationService = locationService;
    }

    public IActionResult Index(string category = "")
    {
        var viewModel = new HomeIndexViewModel();
        viewModel.Categories = _context.Categories.ToList();

        if (category == "")
        {
            viewModel.Anunturi = _context.Anunturi.ToList();
        }
        else
        {
            viewModel.Anunturi = new List<Anunt>();
            foreach (var anunt in _context.Anunturi.ToList())
            {
                if (anunt.Category == category)
                    viewModel.Anunturi.Add(anunt);
            }
        }

        return View("Index", viewModel);
    }

    [Authorize]
    public async Task<IActionResult> New()
    {
        var userId = "notfound";
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        if (claims != null)
        {
            userId = claims.Value;
        }

        var anunt = new Anunt();
        anunt.AccountId = userId;
        var viewModel = new FormViewModel();
        viewModel.Anunt = anunt;
        viewModel.Counties = new List<string>();
        viewModel.Categories = _context.Categories.ToList();

        List<County> Counties = await _locationService.GetCounties();
        foreach (var county in Counties)
        {
            if (county.Nume != null)
                viewModel.Counties.Add(county.Nume);
        }

        return View("Form", viewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var userId = "notfound";
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        if (claims != null)
        {
            userId = claims.Value;
        }

        var anuntInDb = _context.Anunturi.SingleOrDefault( m=>m.Id == id );
        if ( anuntInDb == null || anuntInDb.AccountId != userId )
        {
            return RedirectToAction("Error404", "Error");
        }

        var viewModel = new FormViewModel();
        viewModel.Anunt = anuntInDb;
        viewModel.Counties = new List<string>();
        viewModel.Categories = _context.Categories.ToList();

        List<County> Counties = await _locationService.GetCounties();
        foreach (var county in Counties)
        {
            if (county.Nume != null)
                viewModel.Counties.Add(county.Nume);
        }

        return View("Form", viewModel);
    }

    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(Anunt anunt, IFormFile file)
    {
        var userId = "notfound";
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        if (claims != null)
        {
            userId = claims.Value;
        }
        anunt.AccountId = userId;

        if ( !ModelState.IsValid )
        {
            var viewModel = new FormViewModel();
            viewModel.Anunt = anunt;
            viewModel.Counties = new List<string>();
            viewModel.Categories = _context.Categories.ToList();

            List<County> Counties = await _locationService.GetCounties();
            foreach (var county in Counties)
            {
                if (county.Nume != null)
                    viewModel.Counties.Add(county.Nume);
            }

            return View("Form", viewModel);
        }

        anunt.Date = DateTime.Now;
        
        byte[] imageBytes;
        using ( var ms = new MemoryStream() )
        {
            file.CopyTo(ms);
            imageBytes = ms.ToArray();
        }
        anunt.Image = imageBytes;

        if( anunt.Id == 0 )
        {
            _context.Anunturi.Add(anunt);
            _context.SaveChanges();
        }
        else 
        {
            var anuntInDb = _context.Anunturi.First(m=>m.Id == anunt.Id);
            anuntInDb.Title = anunt.Title;
            anuntInDb.Description = anunt.Description;
            anuntInDb.Price = anunt.Price;
            anuntInDb.Category = anunt.Category;
            anuntInDb.County = anunt.County;
            anuntInDb.City = anunt.City;
            anuntInDb.Mail = anunt.Mail;
            anuntInDb.NumarTelefon = anunt.NumarTelefon;
            anuntInDb.Image = anunt.Image;
            
            _context.SaveChanges();
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Delete(int id)
    {
        var anuntInDb = _context.Anunturi.SingleOrDefault(m => m.Id == id);

        if (anuntInDb == null)
        {
            return RedirectToAction("Error404", "Error");
        }

        _context.Anunturi.Remove(anuntInDb);
        _context.SaveChanges();

        return RedirectToAction("Index", "Home");
    }


    public IActionResult Privacy()
    {
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
