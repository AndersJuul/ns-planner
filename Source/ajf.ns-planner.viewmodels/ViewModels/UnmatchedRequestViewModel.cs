using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ajf.ns_planner.viewmodels.ViewModels
{
    public class UnmatchedRequestViewModel : BaseViewModel, IUnmatchedRequestViewModel
    {
        private bool _showContactDetails;
        private bool _showTimeStamp;
        private RequestViewModel _selectedRequest;

        public UnmatchedRequestViewModel(IEnumerable<RequestViewModel> rviewModels)
        {
            ShowTimeStamp = true;
            ShowContactDetails = true;

            Requests = new ObservableCollection<RequestViewModel>(rviewModels);
        }

        public Visibility ContactDetailsVisibility
        {
            get { return ShowContactDetails ? Visibility.Visible : Visibility.Hidden; }
        }

        public bool ShowContactDetails
        {
            get { return _showContactDetails; }
            set
            {
                _showContactDetails = value;
                OnPropertyChanged();
                OnPropertyChanged("ContactDetailsVisibility");
            }
        }

        public Visibility RequestTimeVisibility
        {
            get { return ShowTimeStamp ? Visibility.Visible : Visibility.Hidden; }
        }

        public bool ShowTimeStamp
        {
            get { return _showTimeStamp; }
            set
            {
                _showTimeStamp = value;
                OnPropertyChanged();
                OnPropertyChanged("RequestTimeVisibility");
            }
        }

        public RequestViewModel SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                _selectedRequest = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RequestViewModel> Requests { get; set; }
    }
}