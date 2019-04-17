namespace IChatYou.BL.Services
{
    using IChatYou.BL.Services.Interfaces;
    using IChatYou.DAL.Entities;
    using IChatYou.DAL.Repositories.Interfaces;

    public class LimitService : ILimitService
    {
        private readonly ILimitRepository limitRepository;

        public LimitService(ILimitRepository limitRepository)
        {
            this.limitRepository = limitRepository;
        }

        public void AddNewLimit(Limit limit)
        {
            limitRepository.Add(limit);

            limitRepository.SaveChangesAsync();
        }
    }
}
