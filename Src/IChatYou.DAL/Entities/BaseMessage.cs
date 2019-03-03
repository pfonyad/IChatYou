namespace IChatYou.DAL.Entities
{
    using System;
    using System.Collections.Generic;

    public class BaseMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Sent { get; set; }

        public virtual ICollection<MessageSwitch> MessageSwitches { get; set; }
    }
}
