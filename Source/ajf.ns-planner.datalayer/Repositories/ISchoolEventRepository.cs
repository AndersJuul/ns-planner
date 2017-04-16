using System.Collections.Generic;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Repositories
{
    public interface ISchoolEventRepository
    {
        IEnumerable<SchoolEvent> ReadEvents(UnitOfWork unitOfWork);
    }
}