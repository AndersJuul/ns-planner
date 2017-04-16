using System.Collections.ObjectModel;
using System.Linq;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.servicelayer;
using ajf.ns_planner.servicelayer.Models;
using ajf.ns_planner.shared;
using ajf.ns_planner.viewmodels.Commands.Interfaces;
using ajf.ns_planner.viewmodels.ViewModels;
using ajf.ns_planner.wpf.Commands.Interfaces;
using ajf.ns_planner.wpf.Windows;
using AutoMapper;

namespace ajf.ns_planner.wpf.Commands.Impl
{
    public class EditMatchesCommand : BaseCommand, IEditMatchesCommand
    {
        private readonly ICounsellorDateService _counsellorDateService;
        private readonly ICounsellorService _counsellorService;
        private readonly IProjectService _projectService;
        private readonly IRequestService _requestService;
        private readonly ISchoolEventService _schoolEventService;
        private readonly IAddSelectedAsNewMatchCommand _addSelectedAsNewMatchCommand;
        private readonly IMatchService _matchService;
        private readonly ICurrentProject _currentProject;

        public EditMatchesCommand(ICounsellorService counsellorService, ICounsellorDateService counsellorDateService,
            IProjectService projectService, IRequestService requestService, ISchoolEventService schoolEventService,
            IAddSelectedAsNewMatchCommand addSelectedAsNewMatchCommand, IMatchService matchService, ICurrentProject currentProject)
        {
            _counsellorService = counsellorService;
            _counsellorDateService = counsellorDateService;
            _projectService = projectService;
            _requestService = requestService;
            _schoolEventService = schoolEventService;
            _addSelectedAsNewMatchCommand = addSelectedAsNewMatchCommand;
            _matchService = matchService;
            _currentProject = currentProject;
        }

        public void Execute(object parameter)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var project = _projectService.GetProjectById(unitOfWork,_currentProject.ProjectId);

                var requests = _requestService.GetUnmatchedRequests(unitOfWork);
                var rviewModels = from r in requests select GetRequestViewModel(r);
                var unmatchedRequestViewModel = new UnmatchedRequestViewModel(rviewModels);

                var counsellors = _counsellorService.GetCounsellors(unitOfWork,_currentProject.ProjectId);
                var counsellorDates = _counsellorDateService
                    .GetCounsellorDates(unitOfWork,_currentProject.ProjectId);
                var counsellorDateViewModels = from cd in counsellorDates select new CounsellorDateViewModel(cd);
                var unmatchedCounsellorDatesViewModel = new UnmatchedCounsellorDatesViewModel(counsellorDateViewModels);

                var events = from e in _schoolEventService.GetEvents(unitOfWork) select new SchoolEventViewModel(e);
                var eventListingViewModel = new EventListingViewModel(events);
                var matchViewModels = new ObservableCollection<MatchViewModel>();

                var editMatchesViewModel = new EditMatchesViewModel(
                    unmatchedRequestViewModel,
                    unmatchedCounsellorDatesViewModel, 
                    eventListingViewModel,
                    _addSelectedAsNewMatchCommand, 
                    matchViewModels);
                
                var editMatchesWindow = new EditMatchesWindow
                {
                    DataContext = editMatchesViewModel
                };

                var showDialog = editMatchesWindow.ShowDialog();
                if (showDialog.HasValue && showDialog.Value)
                {
                    var matches = from m in editMatchesViewModel.CurrentMatches select Match(m);

                    _matchService.SetMatches(matches,unitOfWork);

                    unitOfWork.Db.SaveChanges();
                }
            }
        }

        private static Match Match(MatchViewModel matchViewModel)
        {
            var match = new Match();
            Mapper.Map(matchViewModel, match);
            return match;
        }

        private static RequestViewModel GetRequestViewModel(Request request)
        {
            var requestViewModel = new RequestViewModel();
            Mapper.Map(request, requestViewModel);
            return requestViewModel;
        }
    }

}