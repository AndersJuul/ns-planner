using ajf.ns_planner.shared2.BookHandling;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IBookCollectionProvider
    {
        IBookCollection GetBookCollection(IDerivedPlannerSettings derivedPlannerSettings);
    }
}