using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace TIENDA.Email
{
    public class EmailService
    {
        private readonly SmtpConfig _config;
        private readonly SmtpClient _client;
        public EmailService(SmtpConfig config)
        {
            _config = config;

            _client = new SmtpClient
            {
                Host = config.HostName,
                Port = config.Port,
                Credentials = new NetworkCredential
                {
                    UserName = config.User,
                    Password = config.Password,
                },
                EnableSsl = config.SslEnabled,
                Timeout = config.TimeOut,
            };
        }

        public async Task SendEmailAsync(MailModel model)
        {

            var message = new MailMessage
            {
                From = new MailAddress(_config.DefaultEmailFrom, _config.DefaultNameFrom),
                Subject = model.Subject,
                Body = model.Content,
                IsBodyHtml = model.IsBodyHtml,
            };

            model.To.ForEach(x =>
            {
                message.To.Add(new MailAddress(x.Address, x.DisplayName));
            });


            try
            {
                await _client.SendMailAsync(message);
            }
            catch (Exception)
            {

            }
        }

    }
}
