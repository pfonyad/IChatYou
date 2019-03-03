namespace IChatYou.DAL.Mapping
{
    using IChatYou.DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class MessageSwitchMap : EntityTypeConfiguration<MessageSwitch>
    {
        public MessageSwitchMap()
        {
            HasKey(t =>
            new
            {
                t.SenderUserId,
                t.TargetUserId,
                t.MessageId
            });

            ToTable("MessageSwitches");

            HasRequired(t => t.TargetUser)
                .WithMany(t => t.MessageSwitches)
                .HasForeignKey(t => t.TargetUserId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.BaseMessage)
                .WithMany(t => t.MessageSwitches)
                .HasForeignKey(t => t.MessageId)
                .WillCascadeOnDelete(false);
        }
    }
}
