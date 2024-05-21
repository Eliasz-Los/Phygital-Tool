using Microsoft.AspNetCore.Mvc;
using Phygital.BL;

namespace Phygital.UI_MVC.Controllers;

// This controller is for all the question pages
public class QuestionController : Controller
{
    
    
    private readonly IFlowManager _flowManager;

    public QuestionController(IFlowManager flowManager)
    {
        _flowManager = flowManager;
    }

    public IActionResult Index()
    {
        return View();
    }


   [HttpGet("Creation/AddQuestion")]
    public IActionResult GetQuestion()
    {
        var highest = _flowManager.GetAllFlows().Count();
        ViewBag.Highest = highest;
        
        return View("Creation/AddQuestion");
    }
    
    [HttpPost]
    public IActionResult AddQuestion()
    {
        return View("Creation/AddQuestion");
    }
}