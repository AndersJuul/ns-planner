using ajf.ns_planner.shared2.BookHandling;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface ICalendarService
    {
        void CreateCalendar(int dateColumnInt, int columnAssignedCounsellor, int eventColumnInt, int placeColumnInt, string counsellorShort, IDerivedPlannerSettings derivedPlannerSettings, IBookCollection bookCollection);
    }
}