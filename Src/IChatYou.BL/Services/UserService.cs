namespace IChatYou.BL.Services
{
    using IChatYou.BL.Services.Interfaces;
    using IChatYou.DAL.Entities.User;
    using IChatYou.DAL.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ICollection<ApplicationUser> GetAllUser()
        {
            return userRepository.GetAll();
        }

        public ApplicationUser GetUserById(string id)
        {
            return userRepository.Get(id);
        }

        public bool SwitchState(string id)
        {
            var user = userRepository.Get(id);

            if (user == null)
            {
                throw new Exception("UserNotFound");
            }

            user.IsVisible = !user.IsVisible;
            userRepository.SaveChanges();

            return user.IsVisible;
        }
    }
}
