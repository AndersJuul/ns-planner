using System.Collections.ObjectModel;
using ajf.ns_planner.viewmodels.Commands.Interfaces;

namespace ajf.ns_planner.viewmodels.ViewModels
{
    public interface IEditMatchesViewModel
    {
        IUnmatchedRequestViewModel UnmatchedRequestViewModel { get; set; }
        IUnmatchedCounsellorDatesViewModel UnmatchedCounsellorDatesViewModel { get; set; }
        IEventListingViewModel EventListingViewModel { get; set; }
        IAddSelectedAsNewMatchCommand AddSelectedAsNewMatchCommand { get; set; }
        ObservableCollection<MatchViewModel> CurrentMatches { get; set; }
    }
}