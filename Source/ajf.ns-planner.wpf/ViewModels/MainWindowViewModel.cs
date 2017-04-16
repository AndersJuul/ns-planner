using System.ComponentModel;
using ajf.ns_planner.shared;
using ajf.ns_planner.shared2;
using ajf.ns_planner.wpf.Code.Commands;

namespace ajf.ns_planner.wpf.ViewModels
{
    public class MainWindowViewModel :BaseViewModel, IMainWindowViewModel
    {
        private readonly ICurrentProject _currentProject;

        public MainWindowViewModel(IImportRequestsCommand importRequestsCommand,
            IImportCounsellorsCommand importCounsellorsCommand, IEditCounsellorDatesCommand editCounsellorDatesCommand,
            IEditMatchesCommand editMatchesCommand, ISelectActiveProjectCommand selectActiveProjectCommand,ICurrentProject currentProject)
        {
            _currentProject = currentProject;
            ImportRequestsCommand = importRequestsCommand;
            ImportCounsellorsCommand = importCounsellorsCommand;
            EditCounsellorDatesCommand = editCounsellorDatesCommand;
            EditMatchesCommand = editMatchesCommand;
            SelectActiveProjectCommand = selectActiveProjectCommand;

            _currentProject.PropertyChanged += CurrentProjectPropertyChanged;
        }

        void CurrentProjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "ProjectName")
                return;

            OnPropertyChanged("Title");
        }

        public ISelectActiveProjectCommand SelectActiveProjectCommand { get; set; }
        public IEditCounsellorDatesCommand EditCounsellorDatesCommand { get; set; }
        public IImportCounsellorsCommand ImportCounsellorsCommand { get; set; }
        public IImportRequestsCommand ImportRequestsCommand { get; set; }
        public IEditMatchesCommand EditMatchesCommand { get; set; }

        public string Title
        {
            get
            {
                if(string.IsNullOrEmpty( _currentProject.ProjectName))
                return "NS Planner";
                return "NS Planner -- aktivt projekt er: " + _currentProject.ProjectName;
            }
        }
    }
}