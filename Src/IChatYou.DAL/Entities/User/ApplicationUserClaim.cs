namespace IChatYou.DAL.Entities.User
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}