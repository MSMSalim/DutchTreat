using System;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            this._logger = logger;
        }

        public void SendMail(string to, string subject, string body)
        {
            // Log the message
            _logger.LogInformation($"To: {to}, Subject: {subject}, Body: {body}"); ;
        }
    }
}
