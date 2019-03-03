namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class BanningMap : EntityTypeConfiguration<Banning>
    {
        public BanningMap()
        {
            HasKey(t =>
            new
            {
                t.UserId,
                t.BannedUserId
            });

            ToTable("Bannings");

            HasRequired(t => t.User)
                .WithMany(t => t.Bannings)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
