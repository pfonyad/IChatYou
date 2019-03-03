namespace IChatYou.BL.Services
{
    using DAL.Repositories.Interfaces;
    using IChatYou.DAL.Entities;
    using Interfaces;
    using System;
    using System.Threading.Tasks;

    public class MessageService : IMessageService
    {
        private readonly IUserRepository userRepository;
        private readonly IMessageRepository messageRepository;

        public MessageService(IUserRepository userRepository, IMessageRepository messageRepository)
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
    }
}
