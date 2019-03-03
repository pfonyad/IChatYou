namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class LimitMap : EntityTypeConfiguration<Limit>
    {
        public LimitMap()
        {
            HasKey(t => t.UserId);

            HasRequired(t => t.User)
                .WithRequiredDependent(t => t.Limit)
                .WillCascadeOnDelete(false);

            ToTable("Limits");
        }
    }
}
