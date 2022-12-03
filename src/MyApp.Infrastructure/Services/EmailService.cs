using Microsoft.Extensions.Configuration;
using MyApp.Application.Core.Models;
using MyApp.Application.Core.Services;
using System.Net;
using System.Net.Mail;

namespace MyApp.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILoggerService _loggerService;

        public EmailService(IConfiguration config, ILoggerService loggerService)
        {
            _config = config;
            _loggerService = loggerService;
        }

        public void SendEmail(Email email)
        {
            try
            {
                // from and to addresses are required
                if (string.IsNullOrWhiteSpace(email.From) || string.IsNullOrWhiteSpace(email.To))
                {
                    return;
                }

                using (var smtpClient = new SmtpClient())
                {
                    // SMTP configuration from appsetting.json file
                    var deliveryMethod = _config.GetSection("Smtp:DeliveryMethod").Value;
                    if (deliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory.ToString())
                    {
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtpClient.PickupDirectoryLocation = _config.GetSection("Smtp:PickupDirectoryLocation").Value;
                    }
                    else if (deliveryMethod == SmtpDeliveryMethod.Network.ToString())
                    {
                        // SMTP server
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Host = _config.GetSection("Smtp:Host").Value;
                        smtpClient.Port = Convert.ToInt32(_config.GetSection("Smtp:Port").Value);
                        smtpClient.Credentials = new NetworkCredential(_config.GetSection("Smtp:UserName").Value, _config.GetSection("Smtp:Password").Value);
                        smtpClient.EnableSsl = Convert.ToBoolean(_config.GetSection("Smtp:EnableSsl").Value);
                    }

                    using (var mailMessage = new MailMessage())
                    {
                        // From
                        mailMessage.From = new MailAddress(email.From);

                        // Tos
                        var toMailAddresses = email.To.Split(';');
                        foreach (var mailAddress in toMailAddresses)
                        {
                            mailMessage.To.Add(mailAddress);
                        }

                        // CCs
                        if (!string.IsNullOrWhiteSpace(email.Cc))
                        {
                            var ccMailAddresses = email.Cc.Split(';');
                            foreach (var mailAddress in ccMailAddresses)
                            {
                                mailMessage.CC.Add(mailAddress);
                            }
                        }

                        // BCCs
                        if (!string.IsNullOrWhiteSpace(email.Bcc))
                        {
                            var bccMailAddresses = email.Bcc.Split(';');
                            foreach (var mailAddress in bccMailAddresses)
                            {
                                mailMessage.Bcc.Add(mailAddress);
                            }
                        }

                        mailMessage.Subject = email.Subject;
                        mailMessage.Body = email.Body;
                        mailMessage.IsBodyHtml = true;

                        // Attachments
                        if (email.Attachments != null && email.Attachments.Count > 0)
                        {
                            foreach (var attchment in email.Attachments)
                            {
                                mailMessage.Attachments.Add(attchment.File);
                            }
                        }

                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogException(ex);
            }
        }
    }
}