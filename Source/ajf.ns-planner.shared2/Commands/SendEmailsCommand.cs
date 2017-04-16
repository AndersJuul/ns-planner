using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Commands
{
    public class SendEmailsCommand : BaseCommand, ISendEmailsCommand 
    {
        private readonly IMailSenderService _mailSenderService;
        private readonly IBackupService _backupService;

        public SendEmailsCommand(IMailSenderService mailSenderService, IBackupService backupService)
        {
            _mailSenderService = mailSenderService;
            _backupService = backupService;
        }

        public override void Execute(object parameter)
        {
            _mailSenderService
                .SendWaitingMails()
                .ConfigureAwait(false);

            _backupService.PerformBackup();
        }
    }
}