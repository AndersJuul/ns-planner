using System.Collections.Generic;
using System.Linq;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.datalayer.Repositories;
using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.servicelayer
{
    public class CounsellorService : ICounsellorService
    {
        private readonly ICounsellorRepository _counsellorRepository;

        public CounsellorService(ICounsellorRepository counsellorRepository)
        {
            _counsellorRepository = counsellorRepository;
        }

        public IEnumerable<Counsellor> GetCounsellors(UnitOfWork unitOfWork, int projectId)
        {
            var counsellors = _counsellorRepository.GetCounsellors(unitOfWork,projectId);
            var enumerable = from c in counsellors select new Counsellor(c);
            return enumerable.ToList();
        }
    }
}