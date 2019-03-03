namespace IChatYou.BL.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface IMessageService
    {
        void Send(string senderUserId, string targetUserName, string message);

        Task SendAsync(string senderUserId, string targetUserName, string message);
    }
}
