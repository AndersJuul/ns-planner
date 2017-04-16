using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Repositories
{
    public interface IProjectRepository
    {
        Project GetProjectById(UnitOfWork unitOfWork, int projectId);
        Project GetProjectByName(UnitOfWork unitOfWork, string name);
    }
}