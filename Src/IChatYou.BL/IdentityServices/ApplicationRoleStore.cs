namespace IChatYou.BL.IdentityServices
{
    using IChatYou.DAL;
    using IChatYou.DAL.Entities.User;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    public class ApplicationRoleStore : RoleStore<ApplicationRole, string, ApplicationUserRole>,
        IQueryableRoleStore<ApplicationRole, string>,
        IRoleStore<ApplicationRole, string>, IDisposable
    {
        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
