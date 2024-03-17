using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<FlowController> _logger;
    
    public ContactController(ILogger<FlowController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}