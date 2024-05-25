using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phygital.BL;
using Phygital.Domain.User;

namespace Phygital.UI_MVC.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserManager _userManager;
    private readonly UserManager<Account> _identityManager;
    private readonly UnitOfWork _uow;

    public UserController(ILogger<UserController> logger, IUserManager userManager, UserManager<Account> identityManager, UnitOfWork uow)
    {
        _logger = logger;
        _userManager = userManager;
        _identityManager = identityManager;
        _uow = uow;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        Account currentAccount = new Account();
        if (User.Identity?.Name != null)
        {
            currentAccount = await _identityManager.Users
                .Include(u => u.Organisation) // Eagerly load the Organisation property
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
        }
        var loggedInUserOrganisationId = currentAccount!.Organisation.id;
        
        var users = _userManager.GetUsersByOrganisationId(loggedInUserOrganisationId);
        return View(users);
        // return View();
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