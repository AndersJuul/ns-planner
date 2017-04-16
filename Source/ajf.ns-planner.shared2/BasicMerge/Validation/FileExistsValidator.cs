using System.Collections.Generic;
using System.IO;

namespace ajf.ns_planner.shared2.BasicMerge.Validation
{
    public abstract class FileExistsValidator
    {
        protected void TestForExistence(List<IValidationProblem> validationProblems, string filePath)
        {
            if (!File.Exists(filePath))
            {
                validationProblems.Add(new MissingFileProblem(filePath));
            }
        }
    }
}