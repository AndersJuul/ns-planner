using System.Windows;
using ajf.ns_planner.wpf.ViewModels;

namespace ajf.ns_planner.wpf
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _firstActivation=true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            if (!_firstActivation)
                return;

            _firstActivation = false;

            var mainWindowViewModel = (MainWindowViewModel) DataContext;
            mainWindowViewModel.SelectActiveProjectCommand.Execute(null);
            //mainWindowViewModel.EditMatchesCommand.Execute(null);
        }
    }
}