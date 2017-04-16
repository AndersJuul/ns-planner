using System.Collections.Generic;

namespace ajf.ns_planner.shared2.NsCommands
{
    public interface INsCommandResponse
    {
        List<string> Errors { get; }
    }
}