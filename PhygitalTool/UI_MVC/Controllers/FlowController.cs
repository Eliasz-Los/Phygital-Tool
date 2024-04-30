using Phygital.BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.Domain;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers;

public class FlowController : Controller
{
    private readonly ILogger<FlowController> _logger;
    private readonly IFlowManager _flowManager;
    private readonly IThemeManager _themeManager;
    private readonly UnitOfWork _uow;

    public FlowController(ILogger<FlowController> logger, IFlowManager flowManager, IThemeManager themeManager,
        UnitOfWork uow)
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
        var flow = _flowManager.GetFlowAndThemeById(id);
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        //converteren naar flowdto omdat het op de view niet mogelijk is om een object van type Flow te binden omdat FlowDto gebruikt is op edit
        var flowDto = new FlowDto
        {
            Id = flow.Id,
            FlowType = flow.FlowType,
            IsOpen = flow.IsOpen,
            ThemeId = flow.Theme.Id
        };
        return View(flowDto);
    }

    [HttpPost]
    public IActionResult Edit(long id, FlowDto flow)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var themes = _themeManager.GetAllThemas();
                ViewBag.Themes = themes;

                // Log the validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError("Model validation error: {Error}", error.ErrorMessage);
                }

                return View(flow);
            }

            var theme = _themeManager.GetThemeById(flow.ThemeId);
            if (theme == null)
            {
                ModelState.AddModelError("", "Invalid theme selected.");
                return View(flow);
            }

            _uow.BeginTransaction();
            _flowManager.ChangeFlow(flow.Id, flow.FlowType, flow.IsOpen, theme.Id);
            _uow.Commit();
            return RedirectToAction("Index"); //, new { id = flow.Id }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating flow with id {Id}", id);
            ModelState.AddModelError("", "An error occurred while updating the flow.");
            var themes = _themeManager.GetAllThemas();
            ViewBag.Themes = themes;
            return View(flow);
        }
    }

    [HttpPost]
    public IActionResult Delete(long id)
    {
        _uow.BeginTransaction();
        _flowManager.RemoveFlow(id);
        _uow.Commit();
        return RedirectToAction("Index");
    }
    
    public IActionResult FlowThemeAndType()
    {
        return View("Creation/FlowThemeAndType"); //"Creation/FlowThemeAndType"
    }
    
    
    public IActionResult FlowQuestions(long themeId)
    {
      
        return View("Creation/FlowQuestions"); //Creation/FlowQuestions
    }
}
