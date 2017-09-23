using System.Threading.Tasks;

namespace Project.Models.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
