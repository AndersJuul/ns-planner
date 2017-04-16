namespace ajf.ns_planner.shared2.NsCommands
{
    public class NsValidateMergeExcelResponse : INsQueryResponse
    {
        public NsValidateMergeExcelResponse(string error)
        {
            Error = error;
            HasError = true;
        }
        public NsValidateMergeExcelResponse()
        {
            Error = null;
            HasError = false;
        }

        public bool HasError { get; }

        public string Error { get; }
    }
}