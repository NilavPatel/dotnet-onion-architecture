using MyApp.Application.Core.Models;

namespace MyApp.Application.Core.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}