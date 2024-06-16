
using KanbanBoard.API.Models;
using KanbanBoard.API.Models.Email;
using System.Net.Mail;

namespace KanbanBoard.Domain.Interfaces
{
    public interface IEmailService
    {
        EmailResponse SendEmail(EmailModel emailModel);
    }
}
