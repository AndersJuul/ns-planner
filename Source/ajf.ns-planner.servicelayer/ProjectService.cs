using ajf.ns_planner.datalayer;
using ajf.ns_planner.datalayer.Repositories;
using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.servicelayer
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Project GetProjectById(UnitOfWork unitOfWork, int projectId)
        {
            var dataproject = _projectRepository.GetProjectById(unitOfWork,projectId);
            return new Project(dataproject);
        }

        public Project GetProjectByName(UnitOfWork unitOfWork, string name)
        {
            var dataproject = _projectRepository.GetProjectByName(unitOfWork, name);
            return new Project(dataproject);
        }
    }
}