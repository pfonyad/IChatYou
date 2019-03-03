[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(IChatYou.App.Startup))]
namespace IChatYou.App
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using AutofacSerilogIntegration;
    using AutoMapper;
    using IChatYou.BL.IdentityServices;
    using IChatYou.BL.Services;
    using IChatYou.BL.Services.Interfaces;
    using IChatYou.DAL;
    using IChatYou.DAL.Entities.User;
    using IChatYou.DAL.Repositories;
    using Libraries;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security.DataProtection;
    using Owin;
    using Serilog;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;


    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterLogger();

            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser, string>>().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationRoleStore>().As<IRoleStore<ApplicationRole, string>>().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerLifetimeScope();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerLifetimeScope();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(SettingService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            builder.RegisterType<SettingService>().As<ISettingService>().SingleInstance();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);

            SetupLogger();

            app.MapSignalR();

            DataTables.AspNet.Mvc5.Configuration.RegisterDataTables();

            Mapper.Initialize(cfg =>
            {
                cfg.DisableConstructorMapping();

                cfg.AddProfile(new WebMapperProfile());
            });
            Mapper.AssertConfigurationIsValid();
        }

        private static void SetupLogger()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ApplicationDbConnection"].ConnectionString;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo
                    .MSSqlServer(
                        connectionString,
                        "Logs",
                        autoCreateSqlTable: false)
                .CreateLogger();

            Log.Logger.Information("IChatYou start...");
        }
    }
}
