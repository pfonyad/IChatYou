namespace IChatYou.BL.Services.Interfaces
{
    using IChatYou.DAL.Entities.Base;
    using System.Collections.Generic;

    public interface ISettingService
    {
        void Load();
        void Save();
        void EnsureCreated();

        void Update(Setting setting);

        IEnumerable<Setting> GetAllSetting();
    }
}