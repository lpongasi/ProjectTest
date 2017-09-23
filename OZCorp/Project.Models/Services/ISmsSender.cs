using System.Threading.Tasks;

namespace Project.Models.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
