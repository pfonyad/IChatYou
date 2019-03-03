namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities.Base;
    using System.Data.Entity.ModelConfiguration;

    public class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            // Primary Key
            HasKey(t => t.Name);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(64);

            Property(t => t.Value)
                .HasMaxLength(128);

            // Table & Column Mappings
            ToTable("Settings");

            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Value).HasColumnName("Value");
        }
    }
}
