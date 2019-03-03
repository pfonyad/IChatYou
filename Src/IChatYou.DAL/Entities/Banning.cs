namespace IChatYou.DAL.Entities
{
    using User;

    public class Banning
    {
        public string UserId { get; set; }

        public string BannedUserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationUser BannedUser { get; set; }
    }
}
