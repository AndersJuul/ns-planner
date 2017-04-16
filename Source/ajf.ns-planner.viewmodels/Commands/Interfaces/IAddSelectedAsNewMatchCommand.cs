using System.Windows.Input;
using ajf.ns_planner.viewmodels.ViewModels;

namespace ajf.ns_planner.viewmodels.Commands.Interfaces
{
    public interface IAddSelectedAsNewMatchCommand:ICommand
    {
        IEditMatchesViewModel EditMatchesViewModel { get; set; }
    }
}