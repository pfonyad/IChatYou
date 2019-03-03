namespace IChatYou.DAL.Repositories
{
    using IChatYou.DAL.Entities.User;
    using Interfaces;

    public class UserRepository : Repository<ApplicationUser, string>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
