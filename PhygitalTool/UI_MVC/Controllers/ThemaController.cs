using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers;

public class ThemaController : Controller

{
    private readonly ILogger<ThemaController> _logger;
    private readonly IThemeManager _themeManager;
    private readonly UnitOfWork _uow;


    public ThemaController(ILogger<ThemaController> logger, IThemeManager themeManager, UnitOfWork uow)
    {
        _logger = logger;
        _themeManager = themeManager;
        _uow = uow;
    }

    public IActionResult Index()
    {
        var themas = _themeManager.GetAllThemas();
        return View(themas);
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
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating thema with id {Id}", id);
            ModelState.AddModelError("", "An error occurred while updating the thema.");
            return View();
        }
    }

    [HttpPost]
    public IActionResult Delete(long id)
    {
        _uow.BeginTransaction();
        _themeManager.RemoveTheme(id);
        _uow.Commit();
        return RedirectToAction("Index");
    }

}