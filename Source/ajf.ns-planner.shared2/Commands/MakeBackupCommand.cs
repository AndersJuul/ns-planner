using System;
using System.IO;
using System.IO.Compression;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Commands
{
    public class MakeBackupCommand : BaseCommand, IMakeBackupCommand
    {
        private readonly IBackupService _backupService;

        public MakeBackupCommand(IBackupService backupService)
        {
            _backupService = backupService;
        }

        public override void Execute(object parameter)
        {
            _backupService.PerformBackup();
        }

    }
}