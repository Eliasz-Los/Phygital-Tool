using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

public class ThemaController : Controller

{
    private readonly ILogger<ThemaController> _logger;

    public ThemaController(ILogger<ThemaController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}