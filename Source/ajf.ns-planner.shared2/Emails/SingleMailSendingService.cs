using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ajf.ns_planner.shared2.Interfaces;
using SendGrid;

namespace ajf.ns_planner.shared2.Emails
{
    public class SingleMailSendingService : ISingleMailSendingService
    {
        private readonly IFileContentsProvider _fileContentsProvider;
        private readonly ILogItemListViewModel _logItemListViewModel;

        public SingleMailSendingService(ILogItemListViewModel logItemListViewModel,
            IFileContentsProvider fileContentsProvider)
        {
            _logItemListViewModel = logItemListViewModel;
            _fileContentsProvider = fileContentsProvider;
        }

        public async Task SendSingleMail(MailAddress fromMailAddress, ITransport transport, DateTime batchTime,
            string fileName, bool sendToTestEmail, string testMailReceiver)
        {
            var readAllLines = _fileContentsProvider.GetFileContents(fileName);
            var receiverLine = readAllLines.Single(x => x.ToLower()
                .Contains("<!--receiver:"))
                .Substring(13).Trim('-', '>', ' ');
            var subjectLine = readAllLines.Single(x => x.ToLower().Contains("<!--mailsubject:"))
                .Substring(16)
                .Trim('-', '>', ' ');

            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage {From = fromMailAddress};

            // Add multiple addresses to the To field.
            _logItemListViewModel.CreateInfo("Sender mail til " + receiverLine + ": " + fileName);

            if (sendToTestEmail)
            {
                myMessage.AddTo(testMailReceiver);
            }
            else
            {
                var recieverAdresses = receiverLine.Split(',').ToList();
                myMessage.AddTo(recieverAdresses[0]);
                for (var i = 1; i < recieverAdresses.Count; i++)
                {
                    myMessage.AddCc(recieverAdresses[i]);
                }
            }

            var bytes = Encoding.Default.GetBytes(subjectLine); 
            myMessage.Subject = Encoding.UTF8.GetString(bytes);

            var html = readAllLines.Aggregate("", (current, s) => current + (s + Environment.NewLine));
            myMessage.Html = html;

            // Send the email.
            await transport.DeliverAsync(myMessage);
        }
    }
}