using Microsoft.AspNetCore.Mvc;
using Phygital.BL;

namespace Phygital.UI_MVC.Controllers;

public class OpenquestionController : Controller
{
    private readonly ILogger<OpenquestionController> _logger;
    private readonly IFlowElementManager _FlowElementManager;
    private readonly UnitOfWork _uow;
    
    public OpenquestionController(ILogger<OpenquestionController> logger, IFlowElementManager flowElementManager, UnitOfWork uow)
    {
        _logger = logger;
        _FlowElementManager = flowElementManager;
        _uow = uow;
    }
    
    public IActionResult AddOpenQuestion()
    {
        return View("");
    }
}