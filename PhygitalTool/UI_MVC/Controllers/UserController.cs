using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}