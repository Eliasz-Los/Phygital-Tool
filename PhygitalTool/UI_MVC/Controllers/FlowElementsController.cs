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
    private readonly IFlowManager _flowManager;

    public FlowElementsController(IFlowManager flowManager ,ILogger<FlowController> logger, IThemeManager themeManager, UnitOfWork uow, IFlowElementManager flowElementManager)
    {
        _logger = logger;
        _themeManager = themeManager;
        _uow = uow;
        _flowElementManager = flowElementManager;
        _flowManager = flowManager;
    }

    [HttpGet]
    public IActionResult AddImage(long flowId)
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.flowId = flowId;
        ViewBag.Themes = themes;
        return View();
    }
    
    [HttpGet]
    public IActionResult AddText(long flowId)
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.flowId = flowId;
        ViewBag.Themes = themes;
        return View();
    }

    [HttpGet]
    public IActionResult AddVideo(long flowId)
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.flowId = flowId;
        ViewBag.Themes = themes;
        return View();
    }

    
    [HttpPost]
    public IActionResult AddImage(ImageDto image)
    {
<<<<<<< HEAD
        
=======
>>>>>>> 4426f833011f5411f818753aaa4e9141a4514002
        Image imageToAdd = new Image
        {
            Flow = _flowManager.GetFlowById(image.flowId),
            Title = image.Title,
            AltText = image.AltText,
            SubTheme = _themeManager.GetThemeById(image.subthemeId),
            // todo needed else crash
            Url = "test",
            ImageFile = image.ImageFile
        };
        
        _uow.BeginTransaction();
        _flowElementManager.AddImage(imageToAdd);
        _uow.Commit();

        return RedirectToAction("Index", "Flow");
    }
    
    [HttpPost]
    public IActionResult AddText(TextDto text)
    {
        Text textToAdd = new Text
        {
            Flow = _flowManager.GetFlowById(text.flowId),
            Title = text.Title,
            Content = text.Content,
            SubTheme = _themeManager.GetThemeById(text.SubTheme),
        };
        
        _uow.BeginTransaction();
        _flowElementManager.AddText(textToAdd);
        _uow.Commit();

        return RedirectToAction("Index", "Flow");
    }
    
        
    [HttpPost]
    public IActionResult AddVideo(VideoDto video)
    {
        Video videoToAdd = new Video
        {
            Flow = _flowManager.GetFlowById(video.flowId),
            Title = video.Title,
            Url = video.Url,
            Description = video.Description,
            SubTheme = _themeManager.GetThemeById(video.SubTheme),
        };
        
        _uow.BeginTransaction();
        _flowElementManager.AddVideo(videoToAdd);
        _uow.Commit();

        return RedirectToAction("Index", "Flow");
    }
}