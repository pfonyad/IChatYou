namespace IChatYou.DAL.Entities
{
    using System;
    using User;

    public class Limit
    {
        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public int Value { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
