using System.Collections.ObjectModel;
using ajf.ns_planner.wpf.Code.Commands;

namespace ajf.ns_planner.wpf.ViewModels
{
    public class EditMatchesViewModel
    {
        public EditMatchesViewModel(IUnmatchedRequestViewModel unmatchedRequestViewModel,
            IUnmatchedCounsellorDatesViewModel unmatchedCounsellorDatesViewModel,
            IEventListingViewModel eventListingViewModel,
            IAddSelectedAsNewMatchCommand addSelectedAsNewMatchCommand, ObservableCollection<MatchViewModel> matchViewModels)
        {
            UnmatchedRequestViewModel = unmatchedRequestViewModel;
            UnmatchedCounsellorDatesViewModel = unmatchedCounsellorDatesViewModel;
            EventListingViewModel = eventListingViewModel;
            CurrentMatches = matchViewModels;

            AddSelectedAsNewMatchCommand = addSelectedAsNewMatchCommand;

            AddSelectedAsNewMatchCommand.EditMatchesViewModel = this;
        }

        public ObservableCollection<MatchViewModel> CurrentMatches { get; set; }

        public string Title
        {
            get { return "Rediger matches"; }
        }

        public IUnmatchedRequestViewModel UnmatchedRequestViewModel { get; set; }
        public IUnmatchedCounsellorDatesViewModel UnmatchedCounsellorDatesViewModel { get; set; }
        public IEventListingViewModel EventListingViewModel { get; set; }
        public IAddSelectedAsNewMatchCommand AddSelectedAsNewMatchCommand { get; set; }
    }
}