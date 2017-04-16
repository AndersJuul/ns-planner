using System.Collections.Generic;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Repositories
{
    public interface ICounsellorDateRepository
    {
        IEnumerable<CounsellorDate> GetCounsellorDates(UnitOfWork unitOfWork, int projectId);
        void SetCounsellorDates(IEnumerable<CounsellorDate> counsellorDates, UnitOfWork unitOfWork, int projectId);
    }
}