using System.Linq;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.datalayer.Repositories;
using AutoMapper;

namespace ajf.ns_planner.servicelayer
{
    public class CounsellorImportService : ICounsellorImportService
    {
        private readonly ICounsellorRepository _counsellorRepository;

        public CounsellorImportService(ICounsellorRepository counsellorRepository)
        {
            _counsellorRepository = counsellorRepository;
        }

        public void UpdateAccordingToFile(string filename, UnitOfWork unitOfWork, int projectId)
        {
            var readCounsellors = _counsellorRepository.ReadCounsellors(filename);
            var counsellors = unitOfWork.Db.Counsellors;
            var project = unitOfWork.Db.Projects.SingleOrDefault(x => x.Id == projectId);

            foreach (var counsellor in readCounsellors)
            {
                var singleOrDefault = counsellors.SingleOrDefault(x =>
                    x.Initials == counsellor.Initials
                    //&& x.RequestTime.Millisecond == request.RequestTime.Millisecond
                    );
                if (singleOrDefault == null)
                {
                    // Not found -- we add
                    counsellor.Project = project;
                    unitOfWork.Db.Counsellors.Add(counsellor);
                }
                else
                {
                    // Found -- we update 
                    Mapper.Map(counsellor, singleOrDefault);
                    singleOrDefault.Project = project;
                }
            }
        }
    }
}