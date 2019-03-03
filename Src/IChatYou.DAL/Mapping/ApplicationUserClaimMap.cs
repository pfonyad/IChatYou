namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities.User;
    using System.Data.Entity.ModelConfiguration;

    class ApplicationUserClaimMap : EntityTypeConfiguration<ApplicationUserClaim>
    {
        public ApplicationUserClaimMap()
        {
            ToTable("UserClaims");

            HasRequired(t => t.User)
                .WithMany(t => t.Claims)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
