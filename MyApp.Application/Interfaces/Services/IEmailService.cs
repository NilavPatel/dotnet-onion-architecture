using System.Collections.Generic;
using MyApp.Application.Models;

namespace MyApp.Application.Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(string fromAddress,
            string toAddresses,
            string ccAddresses,
            string bccAddresses,
            string subject,
            string body,
            IList<MailAttachment> attachments);
    }
}