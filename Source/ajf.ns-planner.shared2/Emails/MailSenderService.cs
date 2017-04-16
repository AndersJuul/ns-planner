using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;
using ajf.ns_planner.shared2.Interfaces;
using SendGrid;

namespace ajf.ns_planner.shared2.Emails
{
    public class MailSenderService : IMailSenderService
    {
        private readonly IFileManager _fileManager;
        private readonly ILogItemListViewModel _logItemListViewModel;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IPlannerSettingsProvider _plannerSettingsProvider;
        private readonly ISingleMailSendingService _singleMailSendingService;

        public MailSenderService(IPlannerSettingsProvider plannerSettingsProvider,
            ILogItemListViewModel logItemListViewModel, ISingleMailSendingService singleMailSendingService,
            IFileManager fileManager, IMessageBoxService messageBoxService)
        {
            _plannerSettingsProvider = plannerSettingsProvider;
            _logItemListViewModel = logItemListViewModel;
            _singleMailSendingService = singleMailSendingService;
            _fileManager = fileManager;
            _messageBoxService = messageBoxService;
        }

        public async Task SendWaitingMails()
        {
            var plannerSettings = _plannerSettingsProvider.GetDerivedPlannerSettings(false);

            var batchTime = DateTime.Now;

            _logItemListViewModel.CreateInfo("Batch time: " + batchTime.ToString("F"));

            var fromMailAddress = new MailAddress(plannerSettings.SenderMailAddress);
            var networkCredential = new NetworkCredential("azure_9ce305d9c6ab84b3d5fb7723deca51f5@azure.com",
                "6oi3vEdJl5GldVc");
            var transport = new Web(networkCredential);

            var sendToTestEmail =
                _messageBoxService.ShowYesNoCancel(
                    "Send til test email? Hvis du trykker Nej sendes til modtager fundet i html-filen", "Vælg modtager!");
            if (sendToTestEmail == MessageBoxResult.Cancel)
            {
                return;
            }


            for (var templateId = 0; templateId < 3; templateId++)
            {
                var htmlOutDirectory = plannerSettings.HtmlOutDirectory + templateId;

                var enumerateFiles = _fileManager.GetHtmlFilesInDir(htmlOutDirectory).ToList();
                var count = enumerateFiles.Count;

                var messageBoxResult =
                    _messageBoxService.ShowYesNoCancel(
                        "Klar til at sende " + count + " filer fra folderen " + htmlOutDirectory, "Nu sendes der!");
                if (messageBoxResult == MessageBoxResult.Cancel)
                {
                    return;
                }
                if (messageBoxResult == MessageBoxResult.No)
                {
                    continue;
                }

                var groupSize = plannerSettings.MailGroupSize;

                int remainder;
                var list =
                    enumerateFiles.Select(
                        (t, index) =>
                            new FileAndId
                            {
                                FileName = t,
                                Id = index,
                                GroupId = Math.DivRem(index, groupSize, out remainder)
                            }).ToList();

                var groups = from file in list
                    group file by file.GroupId
                    into newGroup
                    orderby newGroup.Key
                    select newGroup;

                foreach (var group in groups)
                {
                    var messageBoxText = @group.Aggregate("Send til gruppe?" + @group.Key + "." + Environment.NewLine,
                        (current, fileAndId) => current + (fileAndId.FileName + Environment.NewLine));

                    var mr1 = _messageBoxService.ShowYesNoCancel(messageBoxText, "Gruppe!");
                    if (mr1 == MessageBoxResult.No)
                    {
                        continue;
                    }
                    if (mr1 == MessageBoxResult.Cancel)
                    {
                        return;
                    }

                    foreach (var fileAndId in group)
                    {
                        await _singleMailSendingService
                            .SendSingleMail(fromMailAddress, transport, batchTime,
                                fileAndId.FileName, sendToTestEmail == MessageBoxResult.Yes,
                                plannerSettings.TestMailReceiver);
                        _fileManager.MarkMailFileAsSend(fileAndId.FileName);
                    }
                }
            }

            _logItemListViewModel.CreateInfo("Alle mails afsendt...");
        }
    }

    public class FileAndId
    {
        public string FileName { get; set; }
        public int Id { get; set; }
        public int GroupId { get; set; }
    }
}