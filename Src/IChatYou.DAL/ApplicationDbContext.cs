namespace IChatYou.DAL
{
    using Entities;
    using Entities.Base;
    using Entities.User;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Reflection;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        #region Identity

        public virtual IDbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }

        public virtual IDbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        
        public virtual IDbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }

        #endregion

        #region Serilog

        public virtual IDbSet<Log> Logs { get; set; }

        #endregion

        public virtual IDbSet<Setting> Settings { get; set; }

        public virtual IDbSet<BaseMessage> Messages { get; set; }

        public virtual IDbSet<MessageSwitch> MessageSwitches { get; set; }

        public virtual IDbSet<Banning> Bannings { get; set; }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
        }

        public ApplicationDbContext() : base("ApplicationDbConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetAssembly(GetType()).GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type =>
                    type.BaseType != null &&
                    type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
    }
}
