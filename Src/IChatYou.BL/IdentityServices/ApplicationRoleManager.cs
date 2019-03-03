namespace IChatYou.BL.IdentityServices
{
    using IChatYou.DAL.Entities.User;
    using Microsoft.AspNet.Identity;

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }
    }
}
