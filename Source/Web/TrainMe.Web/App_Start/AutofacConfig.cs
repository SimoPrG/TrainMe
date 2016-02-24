namespace TrainMe.Web
{
    using System.Reflection;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    using Controllers;

    using Data;

    using Services.Web;
    using TrainMe.Data.Contracts;
    using TrainMe.Services.Data.Contracts;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new TrainMeDbContext())
                .As<ITrainMeDbContext>().InstancePerRequest();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>)).InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            var dataServicesAssembly = Assembly.GetAssembly(typeof(ICourseService));
            builder.RegisterAssemblyTypes(dataServicesAssembly).AsImplementedInterfaces();

            var webServicesAssembly = Assembly.GetAssembly(typeof(ISanitizer));
            builder.RegisterAssemblyTypes(webServicesAssembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}
