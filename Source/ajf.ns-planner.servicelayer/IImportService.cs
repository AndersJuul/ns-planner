using ajf.ns_planner.datalayer;

namespace ajf.ns_planner.servicelayer
{
    public interface IImportService
    {
        void UpdateAccordingToFile(string filename, UnitOfWork unitOfWork, int projectId);
    }
}