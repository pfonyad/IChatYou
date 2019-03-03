namespace IChatYou.DAL.Entities
{
    using User;

    public class MessageView
    {
        public BaseMessage Message { get; set; }

        public ApplicationUser Sender { get; set; }

        public bool IsReaded { get; set; }
    }
}
