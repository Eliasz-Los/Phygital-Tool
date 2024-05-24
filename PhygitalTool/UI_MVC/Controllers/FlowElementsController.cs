using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

public class FlowElementsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}