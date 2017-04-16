using System;
using System.IO;
using System.Xml;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Settings
{
    public class MyConfigurationManager : IMyConfigurationManager
    {
        public IPlannerSettings GetSettings(string directory, string configFile)
        {
            var fullPathToConfig = Path.Combine(directory, configFile);
            var configXml = new XmlDocument();
            configXml.Load(fullPathToConfig);

            return new PlannerSettings
            {
                Directory = directory,
                RequestFile = GetValue(configXml, "RequestFile"),
                CounsellorFile = GetValue(configXml, "CounsellorFile"),
                EventFile = GetValue(configXml, "EventFile"),
                PlaceFile = GetValue(configXml, "PlaceFile"),
                DestinationFile = GetValue(configXml, "DestinationFile"),
                VejlederColumn = GetValue(configXml, "VejlederColumn"),
                FirstWriteableColumn = GetValue(configXml, "FirstWriteableColumn"),
                ArrangementColumn = GetValue(configXml, "ArrangementColumn"),
                StedColumn = GetValue(configXml, "StedColumn"),
                DatoColumn = GetValue(configXml, "DatoColumn"),
                TidFraColumn = GetValue(configXml, "TidFraColumn"),
                TidTilColumn = GetValue(configXml, "TidTilColumn"),
                SenderMailAddress = GetValue(configXml, "SenderMailAddress"),
                MailGroupSize = Convert.ToInt32(GetValue(configXml, "MailGroupSize")),
                StartDate = Convert.ToDateTime(GetValue(configXml, "StartDate")),
                EndDate = Convert.ToDateTime(GetValue(configXml, "EndDate")),
                TestMailReceiver = GetValue(configXml, "TestMailReceiver"),
                ExpectedPeriod = GetValue(configXml, "ExpectedPeriod")
            };
        }

        private string GetValue(XmlDocument configXml, string key)
        {
            var selectSingleNode = configXml
                .SelectSingleNode("/settings/add[@key = '" + key + "']");
            if (selectSingleNode == null)
            {
                throw new Exception("Key == " + key + " ikke fundet i config-fil");
            }
            var value = selectSingleNode
                .Attributes["value"]
                .Value;
            return value;
        }
    }
}