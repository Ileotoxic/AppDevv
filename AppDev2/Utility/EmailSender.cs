using Microsoft.AspNetCore.Identity.UI.Services;
namespace AppDev2.Utility

  {
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Implement how to send email
            return Task.CompletedTask;
        }
    }
  }
