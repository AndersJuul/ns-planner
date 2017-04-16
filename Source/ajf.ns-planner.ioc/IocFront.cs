using ajf.ns_planner.datalayer.Repositories;
using ajf.ns_planner.servicelayer;
using ajf.ns_planner.shared;
using Microsoft.Practices.Unity;
using UnityConfiguration;

namespace ajf.ns_planner.ioc
{
    public static class IocFront
    {
        private static UnityContainer _unityContainer;

        public static object Get<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public static void Configure()
        {
            _unityContainer = new UnityContainer();

            _unityContainer.Configure(x => { x.AddRegistry<FooRegistry>(); });
            //_unityContainer
            //    .ConfigureAutoRegistration()
            //    .ExcludeAssemblies(a => a.GetName().FullName.Contains("Test"))
            //    .Include(If.Implements<ILogger>, Then.Register().UsingPerCallMode())
            //    .Include(If.ImplementsITypeName, Then.Register().WithTypeName())
            //    .Include(If.Implements<ICustomerRepository>, Then.Register().WithName("Sample"))
            //    .Include(If.Implements<IOrderRepository>,
            //             Then.Register().AsSingleInterfaceOfType().UsingPerCallMode())
            //    .Include(If.DecoratedWith<LoggerAttribute>,
            //             Then.Register()
            //                    .As<IDisposable>()
            //                    .WithTypeName()
            //                    .UsingLifetime<MyLifetimeManager>())
            //    .Exclude(t => t.Name.Contains("Trace"))
            //    .ApplyAutoRegistration();
        }
    }

    public class FooRegistry : UnityRegistry
    {
        public FooRegistry()
        {
            Scan(scan =>
            {
                scan.AssemblyContaining<IRequestRepository>();
                scan.AssemblyContaining<IRequestImportService>();
                //scan.AssemblyContaining<IMainWindowViewModel>();
                scan.AssemblyContaining<ICurrentProject>();

                //scan.ForRegistries();
                //scan.With<FirstInterfaceConvention>();
                scan.With<AddAllConvention>();
                //    .TypesImplementing<IHaveManyImplementations>();
                //scan.With<SetAllPropertiesConvention>().OfType<ILogger>();
                //scan.ExcludeType<FooService>();
            });
        }
    }
}