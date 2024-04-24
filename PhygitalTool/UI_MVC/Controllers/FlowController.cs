using BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers;

public class FlowController : Controller
{
    private readonly ILogger<FlowController> _logger;
    private readonly IFlowManager _flowManager;
    private readonly IThemeManager _themeManager;
    private readonly UnitOfWork _uow;

    public FlowController(ILogger<FlowController> logger, IFlowManager flowManager, IThemeManager themeManager, UnitOfWork uow)
    {
        _logger = logger;
        _flowManager = flowManager;
        _themeManager = themeManager;
        _uow = uow;
    }

    public IActionResult Index()
    {
        var flows = _flowManager.GetAllFlows();
        return View(flows);
    }

    public IActionResult Details(long id)
    {
        var flow = _flowManager.GetFlowById(id);
        return View(flow);
    }
    
    public IActionResult Add()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var flow = _flowManager.GetFlowById(id);
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        return View(flow);
    }
    
    [HttpPost]
    public IActionResult Edit(long id, FlowDto flow)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        else
        {
            _flowManager.ChangeFlow(flow.Id, flow.FlowType, flow.IsOpen, flow.Theme.Id);
            return RedirectToAction("Details", "Flow", new {id = flow.Id});
        }
    }
}