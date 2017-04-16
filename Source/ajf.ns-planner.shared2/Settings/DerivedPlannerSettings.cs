using System;
using ajf.ns_planner.shared2.Interfaces;
using AutoMapper;

namespace ajf.ns_planner.shared2.Settings
{
    public class DerivedPlannerSettings : BaseSettings, IDerivedPlannerSettings
    {
        public DerivedPlannerSettings(IPlannerSettings plannerSettings)
        {
            Mapper.Map(plannerSettings, this);

            var format = string.Format("{0}\\{1}", Directory, DestinationFile);
            var settingsCreationTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            DestinationFileFullPath = string.Format(format, settingsCreationTime);
            UnversionedDestinationFileFullPath = string.Format(format, "");
            HtmlTemplateDir = string.Format("{0}\\{1}", Directory, "HtmlTemplates");
        }

        public string HtmlTemplateDir { get;private set; }

        public string DestinationFileFullPath { get; private set; }
        public string UnversionedDestinationFileFullPath { get; private set; }

        public string CounsellorFileFullPath
        {
            get { return string.Format("{0}\\{1}", Directory, CounsellorFile); }
        }

        public string RequestFileFullPath
        {
            get { return string.Format("{0}\\{1}", Directory, RequestFile); }
        }

        public string EventFileFullPath
        {
            get { return string.Format("{0}\\{1}", Directory, EventFile); }
        }

        public string PlaceFileFullPath
        {
            get { return string.Format("{0}\\{1}", Directory, PlaceFile); }
        }

        public int VejlederColumnInt
        {
            get { return VejlederColumn[0] - 65; }
        }
        public int FirstWriteableColumnInt
        {
            get { return FirstWriteableColumn[0] - 65; }
        }

        public int EventColumnInt
        {
            get { return ArrangementColumn[0] - 65; }
        }

        public int PlaceColumnInt
        {
            get { return StedColumn[0] - 65; }
        }

        public int DateColumnInt
        {
            get { return DatoColumn[0] - 65; }
        }

        public int TidFraColumnInt
        {
            get { return TidFraColumn[0] - 65; }
        }

        public int TidTilColumnInt
        {
            get { return TidTilColumn[0] - 65; }
        }

        public string HtmlOutDirectory
        {
            get { return Directory + "\\HtmlOut"; }
        }
    }
}