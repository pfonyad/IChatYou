namespace IChatYou.DAL.Repositories.Interfaces
{
    using IChatYou.DAL.Entities.Base;

    public interface ISettingRepository : IRepository<Setting, string>
    {
        Setting GetByName(string name);

        void Set(string name, string value);
    }
}