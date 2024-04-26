using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers;

public class ThemaController : Controller

{
    private readonly ILogger<ThemaController> _logger;
    private readonly IThemeManager _themeManager;


    public ThemaController(ILogger<ThemaController> logger, IThemeManager themeManager)
    {
        _logger = logger;
        _themeManager = themeManager;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Add()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var thema = _themeManager.GetThemeById(id);
        return View(thema);
    }


    [HttpPost]
    public IActionResult Edit(long id, SubThemasDto thema)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        else
        {
            _themeManager.ChangeTheme(thema.Id, thema.Title, thema.Description);
            return RedirectToAction("Index", "Thema", new {id = thema.Id});
        }
    }
}