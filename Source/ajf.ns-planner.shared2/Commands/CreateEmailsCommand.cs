using System;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Commands
{
    public class CreateEmailsCommand : BaseCommand, ICreateEmailsCommand
    {
        private readonly IMailMergingService _mailMergingService;
        private readonly ILogItemListViewModel _logItemListViewModel;
        private readonly IBackupService _backupService;

        public CreateEmailsCommand(IMailMergingService mailMergingService, ILogItemListViewModel logItemListViewModel, IBackupService backupService)
        {
            _mailMergingService = mailMergingService;
            _logItemListViewModel = logItemListViewModel;
            _backupService = backupService;
        }

        public override void Execute(object parameter)
        {
            try
            {
                _mailMergingService.DoMailMerge();
                _backupService.PerformBackup();
            }
            catch (Exception exception)
            {
                _logItemListViewModel.CreateError(exception.Message + Environment.NewLine + exception.StackTrace);
            }
        }
    }
}