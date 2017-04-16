using System.Collections.Generic;
using System.Linq;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.BasicMerge.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly IEnumerable<IValidator> _validators;

        public ValidationService(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public List<IValidationProblem> GetValidationProblems(IDerivedPlannerSettings derivedPlannerSettings)
        {
            var applicable = _validators.Where(x => x.CanHandle(derivedPlannerSettings));
            var list = new List<IValidationProblem>();

            foreach (var validator in applicable)
            {
                list.AddRange(validator.GetValidationProblems(derivedPlannerSettings));
            }

            return list;
        }
    }
}