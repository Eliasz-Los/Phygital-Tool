using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

// This controller is for all the question pages
public class QuestionController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    

    public IActionResult AddQuestion()
    {
        return View("Creation/AddQuestion");
    }
}