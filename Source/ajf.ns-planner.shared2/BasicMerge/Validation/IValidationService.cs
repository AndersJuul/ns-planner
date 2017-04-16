using System.Collections.Generic;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.BasicMerge.Validation
{
    public interface IValidationService
    {
        List<IValidationProblem> GetValidationProblems(IDerivedPlannerSettings derivedPlannerSettings);
    }
}