namespace IChatYou.BL.IdentityServices
{
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;

    public class IdentityEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
