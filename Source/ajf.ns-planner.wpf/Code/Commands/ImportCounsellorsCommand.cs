using ajf.ns_planner.datalayer;
using ajf.ns_planner.servicelayer;
using ajf.ns_planner.shared;
using ajf.ns_planner.shared2;

namespace ajf.ns_planner.wpf.Code.Commands
{
    public class ImportCounsellorsCommand :BaseCommand, IImportCounsellorsCommand
    {
        private readonly IFileNameProvider _fileNameProvider;
        private readonly ICounsellorImportService _counsellorImportService;
        private readonly ICurrentProject _currentProject;

        public ImportCounsellorsCommand(IFileNameProvider fileNameProvider, ICounsellorImportService counsellorImportService, ICurrentProject currentProject) 
        {
            _counsellorImportService = counsellorImportService;
            _currentProject = currentProject;
            _fileNameProvider = fileNameProvider;
        }

        public override void Execute(object parameter)
        {
            var filename = _fileNameProvider.GetExcelToImport();
            if(filename==null)return;

            using (var unitOfWork = new UnitOfWork())
            {
                _counsellorImportService.UpdateAccordingToFile(filename,unitOfWork,_currentProject.ProjectId);
                unitOfWork.Db.SaveChanges();
            }
        }
    }

}