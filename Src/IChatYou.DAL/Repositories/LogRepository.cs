namespace IChatYou.DAL.Repositories
{
    using IChatYou.DAL.Entities.Base;
    using Interfaces;

    public class LogRepository : Repository<Log, int>, ILogRepository
    {
        public LogRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
