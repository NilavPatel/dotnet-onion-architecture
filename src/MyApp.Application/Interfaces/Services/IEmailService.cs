using MyApp.Application.Models.Common;

namespace MyApp.Application.Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}