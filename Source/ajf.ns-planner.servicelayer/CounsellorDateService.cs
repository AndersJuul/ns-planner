using System.Collections.Generic;
using System.Linq;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.datalayer.Repositories;
using ajf.ns_planner.servicelayer.Models;
using AutoMapper;

namespace ajf.ns_planner.servicelayer
{
    public class CounsellorDateService : ICounsellorDateService
    {
        private readonly ICounsellorDateRepository _counsellorDateRepository;

        public CounsellorDateService(ICounsellorDateRepository counsellorDateRepository)
        {
            _counsellorDateRepository = counsellorDateRepository;
        }

        public IEnumerable<CounsellorDate> GetCounsellorDates(UnitOfWork unitOfWork, int projectId)
        {
            var counsellorDates = _counsellorDateRepository.GetCounsellorDates(unitOfWork,projectId);
            return counsellorDates.Select(x => new CounsellorDate(x));
        }

        public void SetCounsellorDates(IEnumerable<CounsellorDate> counsellorDates,
            UnitOfWork unitOfWork, int projectId)
        {
            var enumerable = counsellorDates.Select(cd =>
            {
                var counsellorDate = new datalayer.Models.CounsellorDate();
                Mapper.Map(cd, counsellorDate);
                return counsellorDate;
            });
            _counsellorDateRepository.SetCounsellorDates(enumerable, unitOfWork,projectId);
        }
    }
}