using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace Phygital.UI_MVC.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;
    
    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var apiKey = _configuration.GetConnectionString("send_grid_api"); 
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("eliasz.los@student.kdg.be", "support.phygital"),
            Subject = subject,
            HtmlContent = htmlMessage,
        };
        msg.AddTo(new EmailAddress(email, subject));
        var response = await client.SendEmailAsync(msg);
        
        Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
    
    }
}