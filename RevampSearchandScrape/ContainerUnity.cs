using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Unity;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Registration;
using Unity.RegistrationByConvention;

namespace RevampSearchandScrape
{
    public class ContainerUnity
    {
        public static IUnityContainer Initialize()
        {
            var container = BuildUnityContainer();

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.AddNewExtension<Interception>();

            //Hack here: Added custom extension method to solve circular dependency issue for injection members. This matching interface checks for name of class that matches the interface name.
            container.RegisterTypes(
            AllClasses.FromLoadedAssemblies().WithMatchingInterface(),
            WithMappings.FromMatchingInterface,
            WithName.Default,
            WithLifetime.PerResolve, getInjectionMembers: c => new InjectionMember[]
                {
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<LoggingInterceptor>()
                });

            container = RegisterContainer(container);

            //container.AddNewExtension<LazyExtension>();

            return container;
        }

        private static UnityContainer RegisterContainer(UnityContainer container)
        {
            ExcelPackage package = new ExcelPackage();
            container.RegisterInstance(package);

            container.RegisterType<ITest, Test>();
            container.RegisterType<IWebDriver, ChromeDriver>();

            //container.RegisterType(typeof(IConnectionFactory,typeof(ConnectionFactory),new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<LoggingInterceptor>());
            //container.RegisterType(typeof(IQueryRepositoryDapper),typeof(QueryRepositoryDapper),new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<LoggingInterceptor>());
            //container.RegisterType(typeof(ICommandRepositoryDapper), typeof(CommandRepositoryDapper),new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<LoggingInterceptor>());
            //container.RegisterType(typeof(IQueryEmailInformation), typeof(QueryEmailInformation),new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<LoggingInterceptor>());
            //container.RegisterType(typeof(ICommandUserRepository), typeof(CommandUserRepository),new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<LoggingInterceptor>());
            //container.RegisterType(typeof(IQueryUserRepository), typeof(QueryUserRepository),new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<LoggingInterceptor>());

            return container;
        }

    }
}
