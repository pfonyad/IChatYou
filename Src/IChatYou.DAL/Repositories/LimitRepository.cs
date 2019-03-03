namespace IChatYou.DAL.Repositories
{
    using IChatYou.DAL.Entities;
    using Interfaces;
    using System;
    using System.Threading.Tasks;

    public class LimitRepository : Repository<Limit, string>, ILimitRepository
    {
        public LimitRepository(ApplicationDbContext context) : base(context)
        {

        }

        public int GetLimitByUserId(string userId)
        {
            var limit = Get(userId);

            if (limit == null)
            {
                throw new Exception("User Limit Not Found");
            }

            if (limit.Date.Date >= DateTime.UtcNow.Date)
            {
                limit.Value = 5;
                SaveChanges();
            }

            return limit.Value;
        }

        public Task<int> GetLimitByUserIdAsync(string userId)
        {
            return Task.Factory.StartNew(() => GetLimitByUserId(userId));
        }

        public int DecreaseLimitByUserId(string userId)
        {
            var limit = Get(userId);

            if (limit == null)
            {
                throw new Exception("User Limit Not Found");
            }

            if (limit.Date.Date >= DateTime.UtcNow.Date)
            {
                limit.Value = 4;
                SaveChanges();
                return 4;
            }
            else if (--limit.Value > 0)
            {
                SaveChanges();
                return limit.Value;
            }

            return 0;
        }

        public Task<int> DecreaseLimitByUserIdAsync(string userId)
        {
            return Task.Factory.StartNew(() => DecreaseLimitByUserId(userId));
        }
    }
}
