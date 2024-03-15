using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Phygital.UI_MVC.Controllers;

public class EndpageController : Controller
{
    private readonly ILogger<QuestionController> _logger;
    
    public EndpageController(ILogger<QuestionController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}