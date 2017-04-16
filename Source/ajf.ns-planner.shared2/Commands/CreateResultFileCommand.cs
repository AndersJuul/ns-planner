using System;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Commands
{
    public class CreateResultFileCommand : BaseCommand, ICreateResultFileCommand
    {
        private readonly IMergeService _mergeService;
        private readonly ILogItemListViewModel _logItemListViewModel;
        private readonly IBackupService _backupService;

        public CreateResultFileCommand(IMergeService mergeService, ILogItemListViewModel logItemListViewModel, IBackupService backupService)
        {
            _mergeService = mergeService;
            _logItemListViewModel = logItemListViewModel;
            _backupService = backupService;
        }

        public override void Execute(object parameter)
        {
            try
            {
                _mergeService.MergeOnce();
                _backupService.PerformBackup();
            }
            catch (Exception exception)
            {
                _logItemListViewModel.CreateError(exception.Message + Environment.NewLine + exception.StackTrace);
            }
        }
    }
}