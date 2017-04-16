using System.ComponentModel;
using System.Runtime.CompilerServices;
using ajf.ns_planner.shared;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.ViewModels
{
    public class ObservableObject : IObservableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}