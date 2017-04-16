using System.Collections.Generic;
using System.Linq;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.datalayer.Repositories;
using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.servicelayer
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public IEnumerable<Request> GetUnmatchedRequests(UnitOfWork unitOfWork)
        {
            var readRequests = _requestRepository.ReadRequests(unitOfWork);
            var serviceRequests = from r in readRequests select GetRequest(r);
            return serviceRequests;
        }

        private static Request GetRequest(datalayer.Models.Request request)
        {
            var result = new Request();
            AutoMapper.Mapper.Map(request, result);
            return result;
        }
    }
}