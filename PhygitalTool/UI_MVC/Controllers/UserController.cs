using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.BL.Managers;
using Phygital.Domain.User;
using Phygital.UI_MVC.Models;

namespace Phygital.UI_MVC.Controllers;

public class UserController : Controller
{
    private readonly ILogger<ThemaController> _logger;
    private readonly UserManager<Account> _userManager;
    private readonly UnitOfWork _uow;

    public UserController(ILogger<ThemaController> logger, UserManager<Account> userManager, UnitOfWork uow)
    {
        _logger = logger;
        _userManager = userManager;
        _uow = uow;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var loggedInUserEmail = User.Identity.Name;

        var loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
        if (loggedInUser?.Organisation == null)
        {
            // Handle case when user or user's organisation is not found
            _logger.LogError("User with email {Email} not found or has no organisation", loggedInUserEmail);
        }

        var users = _userManager.GetUsersByOrganisationId(loggedInUser.Organisation.id);
        var user = loggedInUser.Organisation.id;
        return View(user);
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