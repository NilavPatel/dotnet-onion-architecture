using MyApp.Domain.Core.Models;

namespace MyApp.Domain.Core.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}