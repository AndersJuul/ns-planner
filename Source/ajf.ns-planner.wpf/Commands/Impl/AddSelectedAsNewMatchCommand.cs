using ajf.ns_planner.viewmodels.Commands.Interfaces;
using ajf.ns_planner.viewmodels.ViewModels;

namespace ajf.ns_planner.wpf.Commands.Impl
{
    public class AddSelectedAsNewMatchCommand : BaseCommand, IAddSelectedAsNewMatchCommand
    {
        public override bool CanExecute(object parameter)
        {
            if (EditMatchesViewModel == null)
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            var matchViewModel = new MatchViewModel
            {
                Request = EditMatchesViewModel.UnmatchedRequestViewModel.SelectedRequest,
                SchoolEvent = EditMatchesViewModel.EventListingViewModel.SelectedEvent,
                CounsellorDate = EditMatchesViewModel.UnmatchedCounsellorDatesViewModel.SelectedCounsellorDate
            };

            EditMatchesViewModel.CurrentMatches.Add(matchViewModel);
        }

        public IEditMatchesViewModel EditMatchesViewModel { get; set; }
    }
}