using System;
namespace DutchTreat.Services
{
    public interface IMailService
    {
        public void SendMail(string to, string subject, string body);
    }
}
