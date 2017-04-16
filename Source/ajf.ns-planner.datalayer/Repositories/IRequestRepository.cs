using System.Collections.Generic;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Repositories
{
    public interface IRequestRepository : IBaseRepository
    {
        IEnumerable<Request> ReadRequests(string filename);
        IEnumerable<Request>  ReadRequests(UnitOfWork unitOfWork);
    }
}