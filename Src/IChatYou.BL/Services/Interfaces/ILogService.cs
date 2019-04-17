namespace IChatYou.BL.Services.Interfaces
{
    using IChatYou.DAL.Entities.Base;
    using System.Linq;

    public interface ILogService
    {
        IQueryable<Log> GetAllLog();
    }
}
