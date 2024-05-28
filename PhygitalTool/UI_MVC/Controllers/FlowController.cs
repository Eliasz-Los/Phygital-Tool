using Microsoft.AspNetCore.Authorization;
using Phygital.BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL.Managers;
using Phygital.Domain.Session;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers;

public class FlowController : Controller
{
    private readonly ILogger<FlowController> _logger;
    private readonly IFlowManager _flowManager;
    private readonly IThemeManager _themeManager;
    private readonly ISessionManager _sessionManager;
    private readonly UnitOfWork _uow;

    public FlowController(ILogger<FlowController> logger, IFlowManager flowManager, IThemeManager themeManager, ISessionManager sessionManager, UnitOfWork uow)
    {
        _logger = logger;
        _flowManager = flowManager;
        _themeManager = themeManager;
        _sessionManager = sessionManager;
        _uow = uow;
    }


    [HttpGet]
    [Authorize(Roles = "Owner, Admin, SubAdmin, Supervisor")]
    public IActionResult Index()
    {
        var flows = _flowManager.GetAllFlows();
        return View(flows);
    }

    [HttpGet]
    [Authorize(Roles = "Owner, Admin, SubAdmin, Supervisor, User")]
    public IActionResult Details(long id)
    {
        var flow = _flowManager.GetFlowById(id);
        var session = _sessionManager.GetSessionById(1);
        
        var participation = new Participation
        {
            StartTime = DateTime.Now.ToUniversalTime(),
            EndTime = DateTime.Now.AddHours(1).ToUniversalTime(),
            AmountOfParticipants = 1,
            Flow = flow,
            Session = session
        };
        
        _uow.BeginTransaction();
        _sessionManager.AddParticipation(participation);
        _uow.Commit();
        
        return View(flow);
    }
    

    [HttpGet]
    [Authorize(Roles = "Admin, SubAdmin")]
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
    [Authorize(Roles = "Admin, SubAdmin")]
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
        //TODO: exception niet hier in nrml gezien
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
    [Authorize(Roles = "Admin, SubAdmin")]
    public IActionResult Delete(long id)
    {
        _uow.BeginTransaction();
        _flowManager.RemoveFlow(id);
        _uow.Commit();
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = "Admin, SubAdmin")]
    public IActionResult FlowThemeAndType()
    {
        return View("Creation/FlowThemeAndType"); //"Creation/FlowThemeAndType"
    }
    
    [Authorize(Roles = "Admin, SubAdmin")]
    public IActionResult FlowQuestions(string selectedTheme) 
    {
        ViewData["SelectedTheme"] = selectedTheme; 
        return View("Creation/FlowQuestions");
    }
}
