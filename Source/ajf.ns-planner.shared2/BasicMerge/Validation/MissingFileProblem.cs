namespace ajf.ns_planner.shared2.BasicMerge.Validation
{
    public class MissingFileProblem : IValidationProblem
    {
        private readonly string _pathToMissingFile;

        public MissingFileProblem(string pathToMissingFile)
        {
            _pathToMissingFile = pathToMissingFile;
        }

        public override string ToString()
        {
            return _pathToMissingFile + " er ikke fundet";
        }
    }
}