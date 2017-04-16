using System.Collections.ObjectModel;

namespace ajf.ns_planner.wpf.ViewModels
{
    public interface IUnmatchedRequestViewModel
    {
        ObservableCollection<RequestViewModel> Requests { get; set; }
        RequestViewModel SelectedRequest { get; set; }
    }
}