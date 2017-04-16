using ajf.ns_planner.shared2;
using ajf.ns_planner.wpf.ViewModels;
using ajf.ns_planner.wpf.Windows;
using MainWindowViewModel = ajf.ns_planner.shared2.MainWindowViewModel;

namespace ajf.ns_planner.wpf.Code.Commands
{
    public class SelectActiveProjectCommand : BaseCommand, ISelectActiveProjectCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var selectProjectWindow = new SelectProjectWindow { DataContext = new SelectProjectViewModel() };
            var showDialog = selectProjectWindow.ShowDialog();

            if (showDialog.HasValue && showDialog.Value)
            {
                //_currentProject.ProjectId = 3;
            }
        }
    }
}