namespace IChatYou.DAL.Repositories.Interfaces
{
    using IChatYou.DAL.Entities;
    using System.Threading.Tasks;

    public interface ILimitRepository : IRepository<Limit, string>
    {
        int GetLimitByUserId(string userId);

        Task<int> GetLimitByUserIdAsync(string userId);

        int DecreaseLimitByUserId(string userId);

        Task<int> DecreaseLimitByUserIdAsync(string userId);
    }
}
