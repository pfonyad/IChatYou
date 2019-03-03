namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities.Base;
    using System.Data.Entity.ModelConfiguration;

    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Level)
                .HasMaxLength(128);

            // Table & Column Mappings
            ToTable("Logs");

            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Message).HasColumnName("Message");
            Property(t => t.MessageTemplate).HasColumnName("MessageTemplate");
            Property(t => t.Level).HasColumnName("Level");
            Property(t => t.TimeStamp).HasColumnName("TimeStamp");
            Property(t => t.Exception).HasColumnName("Exception");
            Property(t => t.Properties).HasColumnName("Properties");
        }
    }
}
