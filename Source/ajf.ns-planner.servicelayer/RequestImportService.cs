using System.Linq;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.datalayer.Repositories;
using AutoMapper;

namespace ajf.ns_planner.servicelayer
{
    public class RequestImportService : IRequestImportService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestImportService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public void UpdateAccordingToFile(string filename, UnitOfWork unitOfWork, int projectId)
        {
            var readRequests = _requestRepository.ReadRequests(filename);
            var projectRequests = unitOfWork.Db.Requests.Where(x => x.Project.Id== projectId);
            var project = unitOfWork.Db.Projects.SingleOrDefault(x => x.Id== projectId);
            foreach (var request in readRequests)
            {
                request.Project= project;
                var s = request.RequestTime.ToString();

                var singleOrDefault = projectRequests.SingleOrDefault(x =>
                    x.RequestTimeValue == request.RequestTimeValue
                    //&& x.RequestTime.Millisecond == request.RequestTime.Millisecond
                    );
                if (singleOrDefault == null)
                {
                    // Not found -- we add
                    unitOfWork.Db.Requests.Add(request);
                }
                else
                {
                    // Found -- we update 
                    Mapper.Map(request, singleOrDefault);
                    singleOrDefault.Project = project;
                }
            }
        }
    }
}