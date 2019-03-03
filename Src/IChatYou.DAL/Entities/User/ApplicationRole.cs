namespace IChatYou.DAL.Entities.User
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string name)
            : this()
        {
            Name = name;
        }

        public string Description { get; set; }
    }
}
