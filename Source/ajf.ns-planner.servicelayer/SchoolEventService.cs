using System.Collections.Generic;
using System.Linq;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.datalayer.Repositories;
using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.servicelayer
{
    public class SchoolEventService : ISchoolEventService
    {
        private readonly ISchoolEventRepository _schoolEventRepository;

        public SchoolEventService(ISchoolEventRepository schoolEventRepository)
        {
            _schoolEventRepository = schoolEventRepository;
        }
        public IEnumerable<SchoolEvent> GetEvents(UnitOfWork unitOfWork)
        {
            var readRequests = _schoolEventRepository.ReadEvents(unitOfWork);
            var serviceRequests = from r in readRequests select GetRequest(r);
            return serviceRequests;
        }

        private SchoolEvent GetRequest(datalayer.Models.SchoolEvent schoolEvent)
        {
            var result = new SchoolEvent(schoolEvent);
            AutoMapper.Mapper.Map(schoolEvent, result);
            return result;
        }
    }

}