using System.Collections.Generic;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Repositories
{
    public interface ICounsellorRepository
    {
        IEnumerable<Counsellor> ReadCounsellors(string filename);
        IEnumerable<Counsellor> GetCounsellors(UnitOfWork unitOfWork, int projectId);
    }
}