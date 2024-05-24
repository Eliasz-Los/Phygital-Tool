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
    public IActionResult Index()
    {
        // var users = _userManager.GetAllUsers();
        // return View(users);
        return View();
    }
}