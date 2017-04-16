using System.Collections.Generic;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.BasicMerge.Validation
{
    public class InputFilesExistsValidator : FileExistsValidator, IValidator
    {
        public IEnumerable<IValidationProblem> GetValidationProblems(IDerivedPlannerSettings derivedPlannerSettings)
        {
            var validationProblems = new List<IValidationProblem>();

            TestForExistence(validationProblems, derivedPlannerSettings.RequestFileFullPath);
            TestForExistence(validationProblems, derivedPlannerSettings.CounsellorFileFullPath);
            TestForExistence(validationProblems, derivedPlannerSettings.EventFileFullPath);
            TestForExistence(validationProblems, derivedPlannerSettings.PlaceFileFullPath);

            return validationProblems;
        }

        public bool CanHandle(IDerivedPlannerSettings derivedPlannerSettings)
        {
            return true;
        }
    }
}