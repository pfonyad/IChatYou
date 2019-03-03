namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities.User;
    using System.Data.Entity.ModelConfiguration;

    class ApplicationRoleMap : EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleMap()
        {
            ToTable("Roles");
        }
    }
}
