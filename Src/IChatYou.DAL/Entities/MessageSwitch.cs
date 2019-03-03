namespace IChatYou.DAL.Entities
{
    using System;
    using User;

    public class MessageSwitch
    {
        public int MessageId { get; set; }

        public string TargetUserId { get; set; }

        public string SenderUserId { get; set; }

        public DateTime? IsReaded { get; set; }

        public virtual BaseMessage BaseMessage { get; set; }

        public virtual ApplicationUser TargetUser { get; set; }

        public virtual ApplicationUser SenderUser { get; set; }
    }
}
