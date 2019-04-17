namespace IChatYou.BL.Services.Interfaces
{
    using IChatYou.DAL.Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        void Send(string senderUserId, string targetUserName, string message);

        Task SendAsync(string senderUserId, string targetUserName, string message);

        int GetLimitByUserId(string userId);

        Task<int> GetLimitByUserIdAsync(string userId);

        int DecreaseLimitByUserId(string userId);

        Task<int> DecreaseLimitByUserIdAsync(string userId);

        IQueryable<MessageView> GetMessagesByUserId(string id);
    }
}
