using System.Collections.Generic;

namespace ajf.ns_planner.datalayer.Repositories
{
    public interface IMatchRepository
    {
        void RemoveAll(UnitOfWork unitOfWork);
        void AddRange(IEnumerable<datalayer.Models.Match> matches, UnitOfWork unitOfWork);
    }
}