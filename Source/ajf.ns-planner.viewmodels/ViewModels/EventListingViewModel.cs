using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ajf.ns_planner.viewmodels.ViewModels
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