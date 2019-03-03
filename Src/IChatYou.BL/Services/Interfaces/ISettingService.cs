namespace IChatYou.BL.Services.Interfaces
{
    public interface ISettingService
    {
        void Load();
        void Save();
        void EnsureCreated();
    }
}