using Gallery.ServiceLayer.Interfaces;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Gallery.ServiceLayer.Implementations
{
    public class MailService : IMailService
    {
        private readonly int _port;
        private readonly string _server;
        private readonly string _serverName;
        private readonly string _serverPassword;

        public MailService()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            _port = int.Parse(settings["port"].Value);
            _server = settings["server"].Value;
            _serverName = settings["serverName"].Value;
            _serverPassword = settings["serverPassword"].Value;
        }

        public async Task SendAsync(string subject, string body, string email)
        {
            MailAddress from = new MailAddress(_serverName);
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(_server, _port);
            smtp.Credentials = new NetworkCredential(_serverName, _serverPassword);
            smtp.EnableSsl = true;
            //await smtp.SendMailAsync(message);
        }
    }
}
