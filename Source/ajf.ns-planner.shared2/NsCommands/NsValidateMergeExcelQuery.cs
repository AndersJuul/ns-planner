namespace ajf.ns_planner.shared2.NsCommands
{
    public class NsValidateMergeExcelQuery : INsQuery
    {
        public NsValidateMergeExcelQuery(string pathToSource, string pathToLookupSource)
        {
            PathToSource = pathToSource;
            PathToLookupSource = pathToLookupSource;
        }

        public string PathToSource { get; }

        public string PathToLookupSource { get; }
    }
}