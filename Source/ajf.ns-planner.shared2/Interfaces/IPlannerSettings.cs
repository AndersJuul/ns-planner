using System;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IPlannerSettings
    {
        string Directory { get; }
        string RequestFile { get; }
        string CounsellorFile { get; }
        string EventFile { get; }
        string PlaceFile { get; }
        string DestinationFile { get; }
        string VejlederColumn { get; }
        string FirstWriteableColumn { get; }
        string ArrangementColumn { get; }
        string StedColumn { get; }
        string DatoColumn { get; }
        string TidFraColumn { get; set; }
        string TidTilColumn { get; set; }
        string SenderMailAddress { get; set; }
        int MailGroupSize { get; set; }
        DateTime EndDate { get; set; }
        DateTime StartDate { get; set; }
        string TestMailReceiver { get; set; }
        string ExpectedPeriod { get; }
    }
}