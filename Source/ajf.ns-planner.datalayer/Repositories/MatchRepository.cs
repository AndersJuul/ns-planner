using System.Collections.Generic;

namespace ajf.ns_planner.datalayer.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        public void RemoveAll(UnitOfWork unitOfWork)
        {
            unitOfWork.Db.Matches.RemoveRange(unitOfWork.Db.Matches);
        }

        public void AddRange(IEnumerable<datalayer.Models.Match> matches, UnitOfWork unitOfWork)
        {
            throw new System.NotImplementedException();
        }
    }
}