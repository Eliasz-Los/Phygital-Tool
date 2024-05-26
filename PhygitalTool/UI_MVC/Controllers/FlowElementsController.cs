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
    
    [HttpGet]
    public IActionResult AddText()
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        return View();
    }

    [HttpGet]
    public IActionResult AddVideo()
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        return View();
    }

    
    [HttpPost]
    public IActionResult AddImage(ImageDto image)
    {
        // todo flowid toevoegen, url staat p test omdat het crasht, ookal is het niet required
        
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
    
    [HttpPost]
    public IActionResult AddText(TextDto text)
    {
        // Todo flow id
        Text textToAdd = new Text
        {
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
        // Todo flow id
        Video videoToAdd = new Video
        {
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