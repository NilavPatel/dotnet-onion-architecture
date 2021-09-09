using MyApp.Application.Models;

namespace MyApp.Application.Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}