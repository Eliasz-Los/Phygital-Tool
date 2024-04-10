using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

public class ProjectController: Controller
{
    private readonly ILogger<FlowController> _logger;
    
    public ProjectController(ILogger<FlowController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}