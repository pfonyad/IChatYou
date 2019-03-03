namespace IChatYou.App.ViewModels
{
    public class MessageViewModel
    {
        public int MessageId { get; set; }

        public string Sender { get; set; }

        public string Sent { get; set; }

        public bool IsReaded { get; set; }

        public string Text { get; set; }
    }
}