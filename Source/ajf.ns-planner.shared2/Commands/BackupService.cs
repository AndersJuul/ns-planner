using System;
using System.IO;
using System.IO.Compression;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Commands
{
    public class BackupService : IBackupService
    {
        private readonly IPlannerSettingsProvider _plannerSettingsProvider;
        private readonly ILogItemListViewModel _logItemListViewModel;

        public BackupService(IPlannerSettingsProvider plannerSettingsProvider, ILogItemListViewModel logItemListViewModel)
        {
            _plannerSettingsProvider = plannerSettingsProvider;
            _logItemListViewModel = logItemListViewModel;
        }

        public void PerformBackup()
        {
            try
            {
                var derivedPlannerSettings = _plannerSettingsProvider.GetDerivedPlannerSettings(true);
                var parentDir = Directory.GetParent(derivedPlannerSettings.Directory);
                var zipFilename = "Ns-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".zip";
                var zipFolder = parentDir + "NsZip\\";
                var zipFileFullPath = zipFolder + zipFilename;

                Directory.CreateDirectory(zipFolder);
                ZipFile.CreateFromDirectory(derivedPlannerSettings.Directory, zipFileFullPath);
                _logItemListViewModel.CreateInfo("Færdig med backup.");
            }
            catch (Exception ex)
            {
                _logItemListViewModel.CreateError("Der skete en fejl. Løs problemet og prøv igen: " + ex.Message);
            }
        }
    }
}