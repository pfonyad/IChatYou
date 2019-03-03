namespace IChatYou.DAL.Repositories
{
    using IChatYou.DAL.Entities;
    using Interfaces;
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessageRepository : Repository<BaseMessage, int>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public void AddNewMessage(BaseMessage message, MessageSwitch messageSwitch)
        {
            using (var transaction = Context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var newMessage = Context.Messages.Add(message);
                    SaveChanges();

                    messageSwitch.MessageId = newMessage.Id;

                    Context.MessageSwitches.Add(messageSwitch);
                    SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public IQueryable<MessageView> GetMessagesByUserId(string userId)
        {
            return Context.MessageSwitches.Where(ms => ms.TargetUserId == userId)
                .Join(Context.Messages,
                x => x.MessageId,
                y => y.Id,
                (x, y) => new MessageView
                {
                    Sender = x.SenderUser,
                    Message = y,
                    IsReaded = x.IsReaded.HasValue
                });
        }

        public Task<IQueryable<MessageView>> GetMessagesByUserIdAsync(string userId)
        {
            return Task.Factory.StartNew(() => GetMessagesByUserId(userId));
        }
    }
}
