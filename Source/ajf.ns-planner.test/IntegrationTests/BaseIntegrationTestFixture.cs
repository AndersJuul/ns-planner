using System.IO;
using System.Reflection;
using ajf.ns_planner.shared2.Interfaces;
using ajf.ns_planner.shared2.Settings;
using ajf.ns_planner.shared2.ViewModels;
using Autofac;
using AutoMapper;
using NUnit.Framework;

namespace ajf.ns_planner.test.IntegrationTests
{
    public abstract class BaseIntegrationTestFixture : BaseTestFixture
    {
        protected ILifetimeScope LifetimeScope;

        [SetUp]
        public void SetupTest()
        {
            Mapper.CreateMap<IPlannerSettings, DerivedPlannerSettings>();
            var containerBuilder = new ContainerBuilder();

            var assembly = Assembly.GetAssembly(typeof (MainWindowViewModel));
            containerBuilder
                .RegisterAssemblyTypes(assembly)
                .SingleInstance()
                .AsImplementedInterfaces();

            var container = containerBuilder.Build();

            LifetimeScope = container.BeginLifetimeScope();

            var nsContext = LifetimeScope.Resolve<INsContext>();
            var assemblyLocation = assembly.Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            nsContext.Directory = Path.GetFullPath(assemblyDirectory + @"\.." + @"\.." + @"\TestData\Ns.2016-1\");
            nsContext.ConfigFile = "Config.xml";

            DeleteOutputFiles(nsContext.Directory + "Resultater");
        }

        private void DeleteOutputFiles(string directory)
        {
            if (!Directory.Exists(directory))
            {
                // Done :)
                return;
            }

            foreach (var file in Directory.EnumerateFiles(directory))
            {
                File.Delete(file);
            }
        }
    }
}