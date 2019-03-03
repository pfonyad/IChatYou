namespace IChatYou.DAL.Repositories
{
    using IChatYou.DAL.Entities.Base;
    using Interfaces;
    using System.Linq;

    public class SettingRepository : Repository<Setting, string>, ISettingRepository
    {
        public SettingRepository(ApplicationDbContext context) :
            base(context)
        {
        }

        public Setting GetByName(string name)
        {
            return Context.Settings.FirstOrDefault(s => s.Name == name);
        }

        public void Set(string name, string value)
        {
            AddOrUpdate(s => s.Name,
                new Setting
                {
                    Name = name,
                    Value = value
                });
        }
    }
}
