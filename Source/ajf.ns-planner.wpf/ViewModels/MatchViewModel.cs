using ajf.ns_planner.shared2;

namespace ajf.ns_planner.wpf.ViewModels
{
    public class MatchViewModel : BaseViewModel
    {
        private CounsellorDateViewModel _counsellorDate;
        private RequestViewModel _request;
        private SchoolEventViewModel _schoolEvent;

        public RequestViewModel Request
        {
            get { return _request; }
            set
            {
                _request = value;
                OnPropertyChanged();
            }
        }

        public SchoolEventViewModel SchoolEvent
        {
            get { return _schoolEvent; }
            set
            {
                _schoolEvent = value;
                OnPropertyChanged();
            }
        }

        public CounsellorDateViewModel CounsellorDate
        {
            get { return _counsellorDate; }
            set
            {
                _counsellorDate = value;
                OnPropertyChanged();
            }
        }
    }
}