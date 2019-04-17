namespace IChatYou.BL.Services
{
    using DAL.Repositories.Interfaces;
    using IChatYou.DAL.Entities;
    using Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessageService : IMessageService
    {
        private readonly IUserRepository userRepository;
        private readonly IMessageRepository messageRepository;
        private readonly IMessageSwitchRepository messageSwitchRepository;
        private readonly ILimitRepository limitRepository;

        public MessageService(IUserRepository userRepository, IMessageRepository messageRepository, IMessageSwitchRepository messageSwitchRepository, ILimitRepository limitRepository)
        {
            this.userRepository = userRepository;
            this.messageRepository = messageRepository;
        }

        public void Send(string senderUserId, string targetUserName, string message)
        {
            var targetUserId = userRepository.Find(usr => usr.UserName == targetUserName).Id;

            messageRepository.AddNewMessage(
                new BaseMessage { Sent = DateTime.UtcNow, Text = message },
                new MessageSwitch { SenderUserId = senderUserId, TargetUserId = targetUserId });
        }

        public Task SendAsync(string senderUserId, string targetUserName, string message)
        {
            return Task.Factory.StartNew(() => Send(senderUserId, targetUserName, message));
        }

        public int GetLimitByUserId(string userId)
        {
            var limit = limitRepository.Get(userId);

            if (limit == null)
            {
                throw new Exception("User Limit Not Found");
            }

            if (limit.Date.Date >= DateTime.UtcNow.Date)
            {
                limit.Value = 5;
                limitRepository.SaveChanges();
            }

            return limit.Value;
        }

        public Task<int> GetLimitByUserIdAsync(string userId)
        {
            return Task.Factory.StartNew(() => GetLimitByUserId(userId));
        }

        public int DecreaseLimitByUserId(string userId)
        {
            var limit = limitRepository.Get(userId);

            if (limit == null)
            {
                throw new Exception("User Limit Not Found");
            }

            if (limit.Date.Date >= DateTime.UtcNow.Date)
            {
                limit.Value = 4;
                limitRepository.SaveChanges();
                return 4;
            }
            else if (--limit.Value > 0)
            {
                limitRepository.SaveChanges();
                return limit.Value;
            }

            return 0;
        }

        public Task<int> DecreaseLimitByUserIdAsync(string userId)
        {
            return Task.Factory.StartNew(() => DecreaseLimitByUserId(userId));
        }

        public IQueryable<MessageView> GetMessagesByUserId(string id)
        {
            return messageSwitchRepository.FindAll(ms => ms.TargetUserId == id).AsQueryable()
                .Join(messageRepository.GetAll(),
                x => x.MessageId,
                y => y.Id,
                (x, y) => new MessageView
                {
                    Sender = x.SenderUser,
                    Message = y,
                    IsReaded = x.IsReaded.HasValue
                });
        }
    }
}
