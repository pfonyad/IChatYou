namespace IChatYou.DAL.Repositories.Interfaces
{
    using IChatYou.DAL.Entities.User;

    public interface IUserRepository : IRepository<ApplicationUser, string>
    {
    }
}
