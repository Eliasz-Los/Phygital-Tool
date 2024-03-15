using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<QuestionController> _logger;
    
    public ContactController(ILogger<QuestionController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}