namespace IChatYou.DAL.Entities
{
    using User;

    public class Favorit
    {
        public string UserId { get; set; }

        public string FavoritUserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationUser FavoritUser { get; set; }
    }
}
