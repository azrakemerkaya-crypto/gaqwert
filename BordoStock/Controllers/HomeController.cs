using Microsoft.AspNetCore.Mvc;

namespace BordoStock.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
