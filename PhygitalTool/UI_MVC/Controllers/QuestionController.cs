using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

public class QuestionController : Controller
{
    private readonly ILogger<QuestionController> _logger;
    
    public QuestionController(ILogger<QuestionController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}