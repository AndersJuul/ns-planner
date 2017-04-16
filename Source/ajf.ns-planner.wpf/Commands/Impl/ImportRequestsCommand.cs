using ajf.ns_planner.datalayer;
using ajf.ns_planner.servicelayer;
using ajf.ns_planner.shared;
using ajf.ns_planner.wpf.Code;
using ajf.ns_planner.wpf.Commands.Interfaces;

namespace ajf.ns_planner.wpf.Commands.Impl
{
    public class ImportRequestsCommand : BaseCommand, IImportRequestsCommand
    {
        private readonly IRequestImportService _requestImportService;
        private readonly IFileNameProvider _fileNameProvider;
        private readonly ICurrentProject _currentProject;

        public ImportRequestsCommand(IRequestImportService requestImportService, IFileNameProvider fileNameProvider,ICurrentProject currentProject)
        {
            _requestImportService = requestImportService;
            _fileNameProvider = fileNameProvider;
            _currentProject = currentProject;
        }

        public void Execute(object parameter)
        {
            var filename=_fileNameProvider.GetExcelToImport();
            if(filename==null)return;

            using (var unitOfWork = new UnitOfWork())
            {
                _requestImportService.UpdateAccordingToFile(filename,unitOfWork,_currentProject.ProjectId);
                unitOfWork.Db.SaveChanges();
            }
        }
    }
}