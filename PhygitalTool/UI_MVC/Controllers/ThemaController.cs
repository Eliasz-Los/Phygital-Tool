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
        var themes = _themeManager.GetAllThemas();
        return View(themes);
    }
    public IActionResult Add()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var theme = _themeManager.GetThemeById(id);
        var themeDto = new SubThemasDto()
        {
            Id = theme.Id,
            Title = theme.Title,
            Description = theme.Description
        };
        return View(themeDto);
    }


    [HttpPost]
    public IActionResult Edit(long id, SubThemasDto theme)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(theme);
            }

            _uow.BeginTransaction();
            _themeManager.ChangeTheme(theme.Id, theme.Title, theme.Description);
            _uow.Commit();
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating theme with id {Id}", id);
            ModelState.AddModelError("", "An error occurred while updating the theme.");
            return View(theme);
        }
    }

    [HttpPost]
    public IActionResult Delete(long id)
    {
        try
        {
            _uow.BeginTransaction();
            _themeManager.RemoveTheme(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting theme with id {Id}", id);
            TempData["ErrorMessage"] = "Dit thema kan niet verwijderd worden want er is nog minstens 1 Flow aan gekoppeld. Verwijder of verander eerst de flow.";
            return RedirectToAction("Index");
        }

    }

}