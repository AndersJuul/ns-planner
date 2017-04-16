using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Xml;
using ajf.ns_planner.shared2.Interfaces;
using ajf.ns_planner.shared2.Settings;
using ajf.ns_planner.shared2.ViewModels;
using Autofac;
using AutoMapper;

namespace ajf.ns_planner.MergeAndSend
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Mapper.CreateMap<IPlannerSettings, DerivedPlannerSettings>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof (App)))
                .SingleInstance();

            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof (MainWindowViewModel)))
                .SingleInstance()
                .AsImplementedInterfaces();

            var container = containerBuilder.Build();

            var lifetimeScope = container.BeginLifetimeScope();

            var fullPathToConfig = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                @"NsPlannerSettings\app.config");
            var directoryName = Path.GetDirectoryName(fullPathToConfig);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            var nsContext = lifetimeScope.Resolve<INsContext>();

            if (!File.Exists(fullPathToConfig))
            {
                var xmlDoc = new XmlDocument();

                var root = xmlDoc.CreateNode(XmlNodeType.Element, "root", null);
                var elementDirectory = xmlDoc.CreateNode(XmlNodeType.Element, "Directory", null);
                var elementConfigFile = xmlDoc.CreateNode(XmlNodeType.Element, "ConfigFile", null);

                var exePath = Assembly.GetAssembly(GetType()).Location;
                var openExeConfiguration = ConfigurationManager.OpenExeConfiguration(exePath);
                elementDirectory.InnerText = openExeConfiguration.AppSettings.Settings["Directory"].Value;
                elementConfigFile.InnerText = openExeConfiguration.AppSettings.Settings["ConfigFile"].Value;

                xmlDoc.AppendChild(root);
                root.AppendChild(elementDirectory);
                root.AppendChild(elementConfigFile);
                xmlDoc.Save(fullPathToConfig);
            }

            var xmlSettings = new XmlDocument();
            xmlSettings.Load(fullPathToConfig);

            var xmlNodeList = xmlSettings.FirstChild.ChildNodes;
            var asQueryable = xmlNodeList.OfType<XmlElement>();
            var xmlElements = asQueryable;
            nsContext.Directory =
                xmlElements
                    .Single(x => x.Name == "Directory")
                    .InnerText;
            nsContext.ConfigFile =
                xmlElements
                    .Single(x => x.Name == "ConfigFile")
                    .InnerText;

            var mainWindowViewModel = lifetimeScope.Resolve<IMainWindowViewModel>();
            var mw = lifetimeScope.Resolve<MainWindow>();
            mw.DataContext = mainWindowViewModel;

            mw.Show();
        }
    }
}