using ajf.ns_planner.viewmodels.ViewModels;
using ajf.ns_planner.wpf.Commands.Interfaces;
using ajf.ns_planner.wpf.Windows;

namespace ajf.ns_planner.wpf.Commands.Impl
{
    public class SelectActiveProjectCommand : BaseCommand, ISelectActiveProjectCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var selectProjectWindow = new SelectProjectWindow {DataContext = new SelectProjectViewModel()};
            var showDialog = selectProjectWindow.ShowDialog();

            if (showDialog.HasValue && showDialog.Value)
            {
                //_currentProject.ProjectId = 3;
            }
        }
    }
}