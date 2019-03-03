namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities.User;
    using System.Data.Entity.ModelConfiguration;

    public class ApplicationUserLoginMap : EntityTypeConfiguration<ApplicationUserLogin>
    {
        public ApplicationUserLoginMap()
        {
            HasKey(t => t.Id);

            ToTable("UserLogins");

            HasRequired(t => t.User)
                .WithMany(t => t.Logins)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
