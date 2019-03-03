namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class FavoritMap : EntityTypeConfiguration<Favorit>
    {
        public FavoritMap()
        {
            HasKey(t =>
            new
            {
                t.UserId,
                t.FavoritUserId
            });

            ToTable("Favorits");

            HasRequired(t => t.User)
                .WithMany(t => t.Favorits)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
