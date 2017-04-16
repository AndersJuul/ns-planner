using System;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Settings
{
    public class BaseSettings:IPlannerSettings
    {
        public string Directory { get; set; }
        public string RequestFile { get; set; }
        public string CounsellorFile { get; set; }
        public string EventFile { get; set; }
        public string PlaceFile { get; set; }
        public string DestinationFile { get; set; }
        public string VejlederColumn { get; set; }
        public string FirstWriteableColumn { get; set; }
        public string ArrangementColumn { get; set; }
        public string StedColumn { get; set; }
        public string DatoColumn { get; set; }
        public string TidFraColumn { get; set; }
        public string TidTilColumn { get; set; }
        public string SenderMailAddress { get; set; }
        public int MailGroupSize { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string TestMailReceiver { get; set; }
        public string ExpectedPeriod { get; set; }
    }
}