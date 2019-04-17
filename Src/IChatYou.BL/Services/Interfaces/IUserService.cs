namespace IChatYou.BL.Services.Interfaces
{
    using IChatYou.DAL.Entities.User;
    using System.Collections.Generic;

    public interface IUserService
    {
        ICollection<ApplicationUser> GetAllUser();

        ApplicationUser GetUserById(string id);

        bool SwitchState(string id);
    }
}
