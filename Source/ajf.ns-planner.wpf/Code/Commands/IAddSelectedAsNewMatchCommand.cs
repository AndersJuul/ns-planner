using System.Windows.Input;
using ajf.ns_planner.wpf.ViewModels;

namespace ajf.ns_planner.wpf.Code.Commands
{
    public interface IAddSelectedAsNewMatchCommand:ICommand
    {
        EditMatchesViewModel EditMatchesViewModel { get; set; }
    }
}