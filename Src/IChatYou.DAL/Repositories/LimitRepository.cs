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
    }
}
