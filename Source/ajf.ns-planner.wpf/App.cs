using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.datalayer.Models;
using ajf.ns_planner.datalayer.Repositories;
using ajf.ns_planner.servicelayer;
using ajf.ns_planner.shared;
using ajf.ns_planner.wpf.ViewModels;
using log4net;
using log4net.Config;
using StructureMap;
using Project = ajf.ns_planner.servicelayer.Models.Project;

namespace ajf.ns_planner.wpf
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //State.ProjectName = "Prod";

            XmlConfigurator.Configure(); //only once

            DataLayerSetup.Setup();
            ServiceLayerSetup.Setup();
            GuiLayerSetup.Setup();

            State.Container = new Container(configurationExpression =>
            {
                configurationExpression.Scan(x =>
                {
                    x.AssemblyContainingType<IRequestRepository>();
                    x.AssemblyContainingType<IRequestImportService>();
                    x.AssemblyContainingType<IMainWindowViewModel>();
                    x.AssemblyContainingType<ICurrentProject>();

                    x.RegisterConcreteTypesAgainstTheFirstInterface();
                    x.WithDefaultConventions();
                });

                configurationExpression.For<ICurrentProject>().Use<CurrentProject>().Singleton();
            });

            Debug.WriteLine(State.Container.WhatDoIHave());

            //var settingsProvider = State.Container.GetInstance<ISettingsProvider>();


            using (var unitOfWork = new UnitOfWork())
            {
                var setting = unitOfWork.Db.Settings.FirstOrDefault();
                if (setting != null)
                {
                    var newState = State.Container.GetInstance<ICurrentProject>();
                    var project = State.Container.GetInstance<IProjectService>().GetProjectById(unitOfWork, setting.LatestProject.Id);
                    newState.ProjectId = setting.LatestProject.Id;
                    newState.ProjectName = project.Name;

                }
            }

            var vm = State.Container.GetInstance<IMainWindowViewModel>();
            var mainform = new MainWindow
            {
                DataContext = vm
            };
            mainform.Show();
        }
    }
}