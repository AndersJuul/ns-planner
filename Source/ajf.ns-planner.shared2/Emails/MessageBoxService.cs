using System.Windows;

namespace ajf.ns_planner.shared2.Emails
{
    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult Show(string messageBoxText, string caption)
        {
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OKCancel);
        }

        public MessageBoxResult ShowYesNoCancel(string messageBoxText, string caption)
        {
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNoCancel);
        }
    }
}