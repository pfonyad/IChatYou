namespace IChatYou.DAL.Entities.User
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public string FullName { get; set; }

        public bool IsVisible { get; set; }

        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }

        public virtual ICollection<MessageSwitch> MessageSwitches { get; set; }

        public virtual ICollection<Banning> Bannings { get; set; }

        public virtual ICollection<Favorit> Favorits { get; set; }

        public virtual Limit Limit { get; set; }
    }
}
