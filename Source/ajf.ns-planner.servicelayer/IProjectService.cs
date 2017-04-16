using ajf.ns_planner.datalayer;
using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.servicelayer
{
    public interface IProjectService
    {
        Project GetProjectById(UnitOfWork unitOfWork, int projectId);
        Project GetProjectByName(UnitOfWork unitOfWork, string name);
    }
}