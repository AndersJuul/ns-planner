using System.Collections.ObjectModel;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.ViewModels
{
    public class LogItemListViewModel : ObservableCollection<LogItemViewModel>, ILogItemListViewModel
    {
        public LogItemListViewModel()
        {
            CreateInfo("Program startet ...");
        }

        public void CreateInfo(string message)
        {
            Add(LogItemViewModel.CreateInfo(message));
        }

        public void CreateError(string message)
        {
            Add(LogItemViewModel.CreateError(message));
        }

        public void CreateWarning(string message)
        {
            Add(LogItemViewModel.CreateWarning(message));
        }
    }
}