using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;

namespace Phygital.UI_MVC.Controllers;

public class UserController : Controller
{
    private readonly ILogger<ThemaController> _logger;
    private readonly IUserManager _userManager;
    private readonly UnitOfWork _uow;

    public UserController(ILogger<ThemaController> logger, IUserManager userManager, UnitOfWork uow)
    {
        _logger = logger;
        _userManager = userManager;
        _uow = uow;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Index(long organisationId)
    {
        var users = _userManager.GetUsersByOrganisationId(organisationId);
        return View(users);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(long id)
    {
        try
        {
            _uow.BeginTransaction();
            _userManager.RemoveUser(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting theme with id {Id}", id);
            TempData["ErrorMessage"] = "Deze user kan niet verwijderd worden.";
            return RedirectToAction("Index");
        }
    }
}