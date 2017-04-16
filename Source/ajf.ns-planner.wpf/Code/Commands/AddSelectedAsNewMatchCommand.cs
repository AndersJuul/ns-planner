using ajf.ns_planner.shared2;
using ajf.ns_planner.wpf.ViewModels;
using MainWindowViewModel = ajf.ns_planner.shared2.MainWindowViewModel;

namespace ajf.ns_planner.wpf.Code.Commands
{
    public class AddSelectedAsNewMatchCommand : BaseCommand, IAddSelectedAsNewMatchCommand
    {
        public override bool CanExecute(object parameter)
        {
            if (EditMatchesViewModel == null)
                return false;

            return true;
        }

        public override void Execute(object parameter)
        {
            var matchViewModel = new MatchViewModel
            {
                Request = EditMatchesViewModel.UnmatchedRequestViewModel.SelectedRequest,
                SchoolEvent = EditMatchesViewModel.EventListingViewModel.SelectedEvent,
                CounsellorDate = EditMatchesViewModel.UnmatchedCounsellorDatesViewModel.SelectedCounsellorDate
            };

            EditMatchesViewModel.CurrentMatches.Add(matchViewModel);
        }

        public EditMatchesViewModel EditMatchesViewModel { get; set; }
    }
}