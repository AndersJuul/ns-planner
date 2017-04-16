using System.Collections.Generic;

namespace ajf.ns_planner.shared2.NsCommands
{
    public class NsMergeExcelResponse : INsCommandResponse
    {
        public NsMergeExcelResponse(List<string> errors)
        {
            Errors = errors;
        }

        public List<string> Errors { get; }
    }
}