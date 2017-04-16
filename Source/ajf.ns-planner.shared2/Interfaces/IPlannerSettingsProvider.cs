namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IPlannerSettingsProvider
    {
        IDerivedPlannerSettings GetDerivedPlannerSettings(bool printFileExistsResult);
        void Check(string fullPath, string subject);
    }
}