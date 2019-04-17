namespace IChatYou.DAL.Repositories
{
    using IChatYou.DAL.Entities;
    using Interfaces;

    public class MessageSwitchRepository : Repository<MessageSwitch, int>, IMessageSwitchRepository
    {
        public MessageSwitchRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
