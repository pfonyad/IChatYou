namespace IChatYou.DAL.Repositories.Interfaces
{
    using IChatYou.DAL.Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMessageRepository : IRepository<BaseMessage, int>
    {
        void AddNewMessage(BaseMessage message, MessageSwitch messageSwitch);

        IQueryable<MessageView> GetMessagesByUserId(string userId);

        Task<IQueryable<MessageView>> GetMessagesByUserIdAsync(string userId);
    }
}
