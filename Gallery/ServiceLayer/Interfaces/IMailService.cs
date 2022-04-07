using System.Threading.Tasks;

namespace Gallery.ServiceLayer.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(string subject, string body, string email);
    }
}
