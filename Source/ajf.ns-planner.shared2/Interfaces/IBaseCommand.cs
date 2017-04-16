using System.Windows.Input;
using ajf.ns_planner.shared2.ViewModels;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IBaseCommand:ICommand
    {
        MainWindowViewModel MainWindowViewModel { get; set; }
    }
}