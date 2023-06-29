using System.Threading.Tasks;

namespace CarFish.Models
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
