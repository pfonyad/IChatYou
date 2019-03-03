namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities.User;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("IX_Email") { IsUnique = true }));

            ToTable("Users");
        }
    }
}
