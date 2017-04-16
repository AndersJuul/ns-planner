using System.Windows;

namespace ajf.ns_planner.shared2.Emails
{
    public interface IMessageBoxService
    {
        MessageBoxResult Show(string messageBoxText, string caption);
        MessageBoxResult ShowYesNoCancel(string messageBoxText, string caption);
    }
}