namespace IChatYou.DAL.Entities.User
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}