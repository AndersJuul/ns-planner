using System.Collections.ObjectModel;

namespace ajf.ns_planner.viewmodels.ViewModels
{
    public interface IUnmatchedRequestViewModel
    {
        ObservableCollection<RequestViewModel> Requests { get; set; }
        RequestViewModel SelectedRequest { get; set; }
    }
}