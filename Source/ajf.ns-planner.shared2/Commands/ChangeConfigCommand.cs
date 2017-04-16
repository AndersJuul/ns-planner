using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Commands
{
    public class ChangeConfigCommand : BaseCommand, IChangeConfigCommand
    {
        public override void Execute(object parameter)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = ConfigurationManager.AppSettings["Directory"]
            };

            var result = dialog.ShowDialog();
            //dialog.SelectedPath=@"C:\Chris\";
            if (result != DialogResult.OK) return;

            var fullPathToConfig = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                @"NsPlannerSettings\app.config");

            var xmlSettings = new XmlDocument();
            xmlSettings.Load(fullPathToConfig);

            var xmlNodeList = xmlSettings.FirstChild.ChildNodes;
            var asQueryable = xmlNodeList.OfType<XmlElement>();
            var xmlElements = asQueryable;
            xmlElements
                .Single(x => x.Name == "Directory")
                .InnerText = dialog.SelectedPath;

            xmlSettings.Save(fullPathToConfig);

            Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }
    }
}