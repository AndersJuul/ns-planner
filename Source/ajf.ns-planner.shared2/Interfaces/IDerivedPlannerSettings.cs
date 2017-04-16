namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IDerivedPlannerSettings : IPlannerSettings
    {
        string UnversionedDestinationFileFullPath { get; }
        string HtmlOutDirectory { get; }
        int FirstWriteableColumnInt { get; }
        string RequestFileFullPath { get; }
        string HtmlTemplateDir { get; }
        string CounsellorFileFullPath { get; }
        string EventFileFullPath { get; }
        string PlaceFileFullPath { get; }
        int VejlederColumnInt { get; }
        int EventColumnInt { get; }
        int DateColumnInt { get; }
        int TidFraColumnInt { get; }
        int TidTilColumnInt { get; }
        int PlaceColumnInt { get; }
    }
}