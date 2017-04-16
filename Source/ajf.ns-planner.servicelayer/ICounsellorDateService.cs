using System.Collections.Generic;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.servicelayer
{
    public interface ICounsellorDateService
    {
        IEnumerable<CounsellorDate> GetCounsellorDates(UnitOfWork unitOfWork, int projectId);
        void SetCounsellorDates(IEnumerable<CounsellorDate> counsellorDates, UnitOfWork unitOfWork, int projectId);
    }
}