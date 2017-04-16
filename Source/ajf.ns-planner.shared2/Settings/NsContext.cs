using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Settings
{
    public class NsContext : INsContext
    {
        public string Directory { get; set; }
        public string ConfigFile { get; set; }
    }
}