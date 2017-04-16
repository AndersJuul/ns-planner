namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IMyConfigurationManager
    {
        IPlannerSettings GetSettings(string directory1, string configFile1);
    }
}