using System;

namespace ajf.ns_planner.wpf.Commands.Impl
{
    public abstract class BaseCommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }
    }
}