namespace IChatYou.DAL.Entities.User
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}