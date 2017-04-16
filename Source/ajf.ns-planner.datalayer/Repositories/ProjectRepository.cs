using System.Linq;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public Project GetProjectById(UnitOfWork unitOfWork, int projectId)
        {
            return unitOfWork.Db.Projects.SingleOrDefault(x => x.Id == projectId);
        }

        public Project GetProjectByName(UnitOfWork unitOfWork, string name)
        {
            return unitOfWork.Db.Projects.SingleOrDefault(x => x.Name == name);
        }
    }
}