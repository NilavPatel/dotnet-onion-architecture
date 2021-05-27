using Microsoft.Extensions.Configuration;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyApp.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string fromAddress, string toAddresses, string ccAddresses, string bccAddresses, string subject, string body, IList<MailAttachment> attachments)
        {
            try
            {
                // from and to addresses are required
                if (string.IsNullOrWhiteSpace(fromAddress) || string.IsNullOrWhiteSpace(toAddresses))
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
                        mailMessage.From = new MailAddress(fromAddress);

                        // Tos
                        var toMailAddresses = toAddresses.Split(';');
                        foreach (var mailAddress in toMailAddresses)
                        {
                            mailMessage.To.Add(mailAddress);
                        }

                        // CCs
                        if (!string.IsNullOrWhiteSpace(ccAddresses))
                        {
                            var ccMailAddresses = ccAddresses.Split(';');
                            foreach (var mailAddress in ccMailAddresses)
                            {
                                mailMessage.CC.Add(mailAddress);
                            }
                        }

                        // BCCs
                        if (!string.IsNullOrWhiteSpace(bccAddresses))
                        {
                            var bccMailAddresses = bccAddresses.Split(';');
                            foreach (var mailAddress in bccMailAddresses)
                            {
                                mailMessage.Bcc.Add(mailAddress);
                            }
                        }

                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        // Attachments
                        if (attachments != null && attachments.Count > 0)
                        {
                            foreach (var attchment in attachments)
                            {
                                mailMessage.Attachments.Add(attchment.File);
                            }
                        }

                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception)
            {
                var sb = new StringBuilder();
                sb.Append("\nFrom:" + fromAddress);
                sb.Append("\nTo:" + toAddresses);
                sb.Append("\nbody:" + body);
                sb.Append("\nsubject:" + subject);
            }
        }
    }
}