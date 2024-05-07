using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phygital.Domain.User;

namespace Phygital.UI_MVC.Controllers;

public class UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    [HttpPost]
    public async Task<IActionResult> Add(string email, string password)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, CustomIdentityConstraints.SupervisorRole);
        }
        else
        {
            // Handle the case where user creation failed
        }
        return RedirectToAction("Index");
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }
}