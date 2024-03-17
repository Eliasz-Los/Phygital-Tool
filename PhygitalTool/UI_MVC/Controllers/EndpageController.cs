using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Phygital.UI_MVC.Controllers;

public class EndpageController : Controller
{
    private readonly ILogger<FlowController> _logger;
    
    public EndpageController(ILogger<FlowController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}