using BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.Domain;

namespace Phygital.UI_MVC.Controllers;

public class FlowController : Controller
{
    private readonly ILogger<FlowController> _logger;
    private readonly IFlowManager _flowManager;
    private readonly UnitOfWork _uow;
    
    public FlowController(ILogger<FlowController> logger, IFlowManager flowManager, UnitOfWork uow)
    {
        _logger = logger;
        _flowManager = flowManager;
        _uow = uow;
    }
    

    public IActionResult Index()
    {
        
        var flows = _flowManager.GetAllFlows();
        return View(flows);
    }
}