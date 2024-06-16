using KanbanBoard.API.Models;
using KanbanBoard.Domain.Interfaces;
using System.Net.Mail;
using System.Net;
using KanbanBoard.API.Models.Email;

namespace KanbanBoard.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EmailResponse SendEmail(EmailModel emailModel)
        {
            var result = new EmailResponse();
            try
            {
                var message = new MailMessage()
                {
                    From = new MailAddress(GetEmail()),
                    Subject = emailModel.Subject,
                    IsBodyHtml = true,
                    Body = emailModel.Body
                };

                message.To.Add(new MailAddress(emailModel.ToEmail));

                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(GetEmail(), GetPassword())
                };

                smtp.Send(message);

                result.Success = true;
                result.Message = "Email sent successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An unexpected error occurred while sending the email: {ex.Message}";
            }

            return result;
        }

        private string GetEmail()
        {
            return _configuration["EmailConfig:email"];
        }

        private string GetPassword()
        {
            return _configuration["EmailConfig:password"];
        }


    }
}
