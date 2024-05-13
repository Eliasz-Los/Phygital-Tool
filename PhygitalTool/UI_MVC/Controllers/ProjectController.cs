using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

public class ProjectController: Controller
{
    private readonly ILogger<ProjectController> _logger;
    
    public ProjectController(ILogger<ProjectController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, SubAdmin")]
    public IActionResult Index()
    {
        return View();
    }
}