namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities.User;
    using System.Data.Entity.ModelConfiguration;

    public class ApplicationUserRoleMap : EntityTypeConfiguration<ApplicationUserRole>
    {
        public ApplicationUserRoleMap()
        {
            HasKey(t =>
            new
            {
                UserId = t.UserId,
                RoleId = t.RoleId
            });

            ToTable("UserRoles");

            HasRequired(t => t.User)
                .WithMany(t => t.Roles)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(t => t.RoleId)
                .WillCascadeOnDelete(false);
        }
    }
}
