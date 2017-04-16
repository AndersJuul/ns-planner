using System.Linq;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.servicelayer;
using ajf.ns_planner.shared;
using ajf.ns_planner.viewmodels.ViewModels;
using ajf.ns_planner.wpf.Commands.Interfaces;
using ajf.ns_planner.wpf.Windows;

namespace ajf.ns_planner.wpf.Commands.Impl
{
    public class EditCounsellorDatesCommand :BaseCommand, IEditCounsellorDatesCommand
    {
        private readonly ICounsellorService _counsellorService;
        private readonly ICounsellorDateService _counsellorDateService;
        private readonly IProjectService _projectService;
        private readonly ICurrentProject _currentProject;

        public EditCounsellorDatesCommand(ICounsellorService counsellorService, ICounsellorDateService counsellorDateService, IProjectService projectService, ICurrentProject currentProject)
        {
            _counsellorService = counsellorService;
            _counsellorDateService = counsellorDateService;
            _projectService = projectService;
            _currentProject = currentProject;
        }

        public void Execute(object parameter)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var project = _projectService.GetProjectById(unitOfWork,_currentProject.ProjectId);
                var counsellors = _counsellorService.GetCounsellors(unitOfWork,_currentProject.ProjectId).ToList();
                var counsellorDates = _counsellorDateService
                    .GetCounsellorDates(unitOfWork,_currentProject.ProjectId)
                    .ToList();

                var editCounsellorDatesViewModel = new EditCounsellorDatesViewModel();
                for (int i = 0; i <= project.LengthInDays; i++)
                {
                    var counsellorViewModels = (from c in counsellors select new CounsellorViewModel(c)).ToList();
                    var dateViewModel = new DateViewModel(project.FirstDate.AddDays(i), counsellorViewModels);
                    editCounsellorDatesViewModel.Add(dateViewModel);
                }

                foreach (var counsellorDate in counsellorDates)
                {
                    editCounsellorDatesViewModel.Check(counsellorDate.Date, counsellorDate.CounsellorId);
                }

                var editCounsellorDatesWindow = new EditCounsellorDatesWindow
                {
                    DataContext = editCounsellorDatesViewModel
                };
                var showDialog = editCounsellorDatesWindow.ShowDialog();
                if (showDialog.HasValue && showDialog.Value)
                {
                    var dates =editCounsellorDatesViewModel.GetCounsellorDates(_currentProject.ProjectId);
                    _counsellorDateService.SetCounsellorDates(dates,unitOfWork,_currentProject.ProjectId);

                    unitOfWork.Db.SaveChanges();
                }
            }
        }
    }
}