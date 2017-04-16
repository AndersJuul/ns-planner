using System;
using ajf.ns_planner.shared2.Interfaces;
using ajf.ns_planner.shared2.ViewModels;

namespace ajf.ns_planner.shared2.Commands
{
    public abstract class BaseCommand:IBaseCommand
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }

        public abstract void Execute(object parameter);
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }
    }
}