namespace IChatYou.BL.Services.Interfaces
{
    using IChatYou.DAL.Entities;

    public interface ILimitService
    {
        void AddNewLimit(Limit limit);
    }
}
