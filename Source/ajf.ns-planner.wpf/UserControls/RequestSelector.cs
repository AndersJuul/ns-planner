using System.Windows;

namespace ajf.ns_planner.wpf.UserControls
{
    /// <summary>
    ///     Interaction logic for RequestSelector.xaml
    /// </summary>
    public partial class RequestSelector
    {
        public RequestSelector()
        {
            InitializeComponent();
        }
    }
    public class BindingProxy : Freezable
    {
        #region Overrides of Freezable

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        #endregion

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object),
                                         typeof(BindingProxy));
    }
}