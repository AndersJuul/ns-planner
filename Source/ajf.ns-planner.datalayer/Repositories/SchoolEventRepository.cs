using System.Collections.Generic;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Repositories
{
    public class SchoolEventRepository : BaseRepository, ISchoolEventRepository
    {
        public IEnumerable<SchoolEvent> ReadEvents(UnitOfWork unitOfWork)
        {
            return unitOfWork.Db.SchoolEvents;
        }
    }
}