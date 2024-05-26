using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.BL.Managers;
using Phygital.Domain.Questionsprocess.Infos;
using Phygital.UI_MVC.Models.Dto.Feedback;
using Phygital.UI_MVC.Models.Dto.Infos;

namespace Phygital.UI_MVC.Controllers;

public class FlowElementsController : Controller
{
    private readonly ILogger<FlowController> _logger;
    private readonly IThemeManager _themeManager;
    private readonly UnitOfWork _uow;
    private readonly IFlowElementManager _flowElementManager;

    public FlowElementsController(ILogger<FlowController> logger, IThemeManager themeManager, UnitOfWork uow, IFlowElementManager flowElementManager)
    {
        _logger = logger;
        _themeManager = themeManager;
        _uow = uow;
        _flowElementManager = flowElementManager;
    }

    [HttpGet]
    public IActionResult AddImage()
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        return View();
    }
    
    [HttpPost]
    public IActionResult AddImage(ImageDto image)
    {
        // todo image.flowId, subthemeId,?
        
        Image imageToAdd = new Image
        {
            Title = image.Title,
            AltText = image.AltText,
            SubTheme = _themeManager.GetThemeById(image.subthemeId),
            Url = "test",
            ImageFile = image.ImageFile
        };
        
        _uow.BeginTransaction();
        _flowElementManager.AddImage(imageToAdd);
        _uow.Commit();

        return RedirectToAction("Index", "Flow");
    }
    
}