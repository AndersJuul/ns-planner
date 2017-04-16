using System.Collections.Generic;
using System.Collections.ObjectModel;
using ajf.ns_planner.shared2;

namespace ajf.ns_planner.wpf.ViewModels
{
    public class UnmatchedCounsellorDatesViewModel : BaseViewModel, IUnmatchedCounsellorDatesViewModel
    {
        private CounsellorDateViewModel _selectedCounsellorDate;

        public UnmatchedCounsellorDatesViewModel()
        {
            CounsellorDateViewModels = new ObservableCollection<CounsellorDateViewModel>();
        }

        public UnmatchedCounsellorDatesViewModel(IEnumerable<CounsellorDateViewModel> counsellorDateViewModels) : this()
        {
            CounsellorDateViewModels = new ObservableCollection<CounsellorDateViewModel>(counsellorDateViewModels);
        }

        public CounsellorDateViewModel SelectedCounsellorDate
        {
            get { return _selectedCounsellorDate; }
            set
            {
                _selectedCounsellorDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CounsellorDateViewModel> CounsellorDateViewModels { get; set; }
    }
}