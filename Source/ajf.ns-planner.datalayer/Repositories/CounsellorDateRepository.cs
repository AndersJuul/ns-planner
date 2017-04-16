using System.Collections.Generic;
using System.Linq;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Repositories
{
    public class CounsellorDateRepository : ICounsellorDateRepository
    {
        public IEnumerable<CounsellorDate> GetCounsellorDates(UnitOfWork unitOfWork, int projectId)
        {
            var counsellorDates = unitOfWork.Db
                .CounsellorDates
                .Where(x => x.Project.Id==projectId);

            return counsellorDates;
        }

        public void SetCounsellorDates(IEnumerable<CounsellorDate> counsellorDates, UnitOfWork unitOfWork, int projectId)
        {
            var queryable = unitOfWork.Db.CounsellorDates.Where(x => x.Project.Id == projectId);
            unitOfWork.Db.CounsellorDates.RemoveRange(queryable);

            unitOfWork.Db.CounsellorDates.AddRange(counsellorDates);
        }
    }
}