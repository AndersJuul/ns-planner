using System.Windows;

namespace ajf.ns_planner.wpf.Windows
{
    /// <summary>
    ///     Interaction logic for EditMatchesWindow.xaml
    /// </summary>
    public partial class EditMatchesWindow
    {
        public EditMatchesWindow()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}