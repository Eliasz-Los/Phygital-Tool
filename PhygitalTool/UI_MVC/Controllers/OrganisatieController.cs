using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers;

public class OrganisatieController : Controller
{
    private readonly ILogger<ThemaController> _logger;
    private readonly IUserManager _userManager;
    private readonly UnitOfWork _uow;

    public OrganisatieController(ILogger<ThemaController> logger, IUserManager userManager, UnitOfWork uow)
    {
        _logger = logger;
        _userManager = userManager;
        _uow = uow;
    }

    [HttpGet]
    [Authorize(Roles = "Owner")]
    public IActionResult Index()
    {
        var organisations = _userManager.GetAllOrganisations();
        return View(organisations);
    }
    
    [HttpGet]
    [Authorize(Roles = "Owner")]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpGet]
    [Authorize(Roles = "Owner")]
    public IActionResult Edit(long id)
    {
        var organisation = _userManager.GetOrganisationById(id);
        var organisatieDto = new OrganisatieDto()
        {
            id = organisation.id,
            Name = organisation.Name,
            Description = organisation.Description
        };
        return View(organisatieDto);
    }


    [HttpPost]
    [Authorize(Roles = "Owner")]
    public IActionResult Edit(long id, OrganisatieDto organisatie)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(organisatie);
            }

            _uow.BeginTransaction();
            _userManager.ChangeOrganisation(organisatie.id, organisatie.Name, organisatie.Description);
            _uow.Commit();
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating organisation with id {Id}", id);
            ModelState.AddModelError("", "An error occurred while updating the organisation.");
            return View(organisatie);
        }
    }
    
    [HttpPost]
    [Authorize(Roles = "Owner")]
    public IActionResult Delete(long id)
    {
        try
        {
            _uow.BeginTransaction();
            _userManager.RemoveOrganisation(id);
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