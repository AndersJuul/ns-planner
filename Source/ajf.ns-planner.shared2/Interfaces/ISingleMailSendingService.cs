using System;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface ISingleMailSendingService
    {
        Task SendSingleMail(MailAddress fromMailAddress, ITransport transport, DateTime batchTime, string fileName, bool sendToTestEmail, string testMailReceiver);
    }
}