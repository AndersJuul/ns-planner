using ajf.ns_planner.datalayer;
using ajf.ns_planner.servicelayer;
using ajf.ns_planner.shared;
using ajf.ns_planner.wpf.Code;
using ajf.ns_planner.wpf.Commands.Interfaces;

namespace ajf.ns_planner.wpf.Commands.Impl
{
    public class ImportCounsellorsCommand : BaseCommand, IImportCounsellorsCommand
    {
        private readonly ICounsellorImportService _counsellorImportService;
        private readonly ICurrentProject _currentProject;
        private readonly IFileNameProvider _fileNameProvider;

        public ImportCounsellorsCommand(IFileNameProvider fileNameProvider,
            ICounsellorImportService counsellorImportService, ICurrentProject currentProject)
        {
            _counsellorImportService = counsellorImportService;
            _currentProject = currentProject;
            _fileNameProvider = fileNameProvider;
        }

        public void Execute(object parameter)
        {
            var filename = _fileNameProvider.GetExcelToImport();
            if (filename == null) return;

            using (var unitOfWork = new UnitOfWork())
            {
                _counsellorImportService.UpdateAccordingToFile(filename, unitOfWork, _currentProject.ProjectId);
                unitOfWork.Db.SaveChanges();
            }
        }
    }
}