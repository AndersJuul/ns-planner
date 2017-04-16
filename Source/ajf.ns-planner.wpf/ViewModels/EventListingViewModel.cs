using System.Collections.Generic;
using System.Collections.ObjectModel;
using ajf.ns_planner.shared2;

namespace ajf.ns_planner.wpf.ViewModels
{
    public class EventListingViewModel : BaseViewModel, IEventListingViewModel
    {
        private SchoolEventViewModel _selectedEvent;

        public EventListingViewModel(IEnumerable<SchoolEventViewModel> events)
        {
            Events = new ObservableCollection<SchoolEventViewModel>(events);
        }

        public SchoolEventViewModel SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SchoolEventViewModel> Events { get; set; }
    }
}