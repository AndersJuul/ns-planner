namespace ajf.ns_planner.shared2.NsCommands
{
    public class NsMergeExcelCommand : INsCommand
    {
        public NsMergeExcelCommand(string pathToSource, string pathToDestination, string pathToLookupSource, string columnNameInSource)
        {
            PathToSource = pathToSource;
            PathToDestination = pathToDestination;
            PathToLookupSource = pathToLookupSource;
            ColumnNameInSource = columnNameInSource;
        }

        public string PathToSource { get; }
        public string PathToDestination { get; }
        public string PathToLookupSource { get; }
        public string ColumnNameInSource { get;  }


    }
}