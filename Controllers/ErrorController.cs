using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }

    }
}