namespace ajf.ns_planner.wpf.Windows
{
    /// <summary>
    ///     Interaction logic for EditCounsellorDatesWindow.xaml
    /// </summary>
    public partial class EditCounsellorDatesWindow
    {
        public EditCounsellorDatesWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}