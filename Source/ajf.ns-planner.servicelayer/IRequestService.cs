using System.Collections.Generic;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.servicelayer
{
    public interface IRequestService
    {
       IEnumerable< Request> GetUnmatchedRequests(UnitOfWork unitOfWork);
    }
}