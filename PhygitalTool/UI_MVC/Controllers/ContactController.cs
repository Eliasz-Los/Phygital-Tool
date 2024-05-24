using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Phygital.UI_MVC.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<FlowController> _logger;
    private IEmailSender _emailSender;
    
    public ContactController(ILogger<FlowController> logger, IEmailSender emailSender)
    {
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<IActionResult> Index()
    {
        var email = _emailSender.SendEmailAsync("arthur.linsen@student.kdg.be", "Test", "worked?");
        await email;
        _logger.LogInformation("Email sent: {email}", email.GetAwaiter().IsCompleted);
        return View(email);
    }
}