using System.Collections;
using System.Collections.Generic;
using ajf.ns_planner.shared2.ViewModels;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface ILogItemListViewModel : IEnumerable<LogItemViewModel>
    {
        void Add(LogItemViewModel logItemViewModel);
        void CreateInfo(string message);
        void Clear();
        void CreateError(string message);
        void CreateWarning(string message);
    }
}