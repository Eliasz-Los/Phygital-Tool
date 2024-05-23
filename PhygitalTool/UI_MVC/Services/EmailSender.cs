using MailKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Phygital.UI_MVC.Services;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMsg = new MimeMessage();
        emailMsg.From.Add(new MailboxAddress("Eliasz", "eliasz.los@student.kdg.be"));
        emailMsg.To.Add(new MailboxAddress("Eliasz", email));
        emailMsg.Subject = subject;
        emailMsg.Body = new TextPart("html") {Text = htmlMessage};

        using (var client = new SmtpClient( new ProtocolLogger("smtp.log")))
        {
            await client.ConnectAsync("smtp-mail.outlook.com", 587, false); //smtp-mail.outlook.com -- smtp.office365.com"
            await client.AuthenticateAsync("eliasz.los@student.kdg.be", "password");
            await client.SendAsync(emailMsg);
            
            await client.DisconnectAsync(true);
        }

        /*var client = new SmtpClient("smtp.office365.com")
        {
            UseDefaultCredentials = false,
            Port = 465, //587 normaal correct voor TSL/STARTTLS maar 465 voor SSL
            Credentials = new NetworkCredential("eliasz.los@student.kdg.be", "KdG4H75B"),
            EnableSsl = true
        };

        return client.SendMailAsync(new MailMessage("eliasz.los@gmail.com", email, subject, htmlMessage) {IsBodyHtml = true});*/ //KdG4H75B
    }
}