using ajf.ns_planner.shared2;

namespace ajf.ns_planner.shared
{
    public interface ICurrentProject:IObservableObject
    {
        string DocumentDirectory { get; }
        int ProjectId { get; set; }
        string ProjectName { get; set; }
    }
}