namespace IChatYou.BL.Services
{
    using IChatYou.BL.Services.Interfaces;
    using IChatYou.DAL.Entities.Base;
    using IChatYou.DAL.Repositories.Interfaces;
    using System.Linq;

    public class LogService : ILogService
    {
        private readonly ILogRepository logRepository;

        public LogService(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public IQueryable<Log> GetAllLog()
        {
            return logRepository.GetAll().AsQueryable();
        }
    }
}
