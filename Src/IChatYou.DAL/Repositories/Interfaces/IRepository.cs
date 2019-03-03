namespace IChatYou.DAL.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        void Add(TEntity entity);
        void AddAll(IEnumerable<TEntity> list);
        void AddOrUpdate(Expression<Func<TEntity, object>> identifierExpression, params TEntity[] entities);
        int Count();
        Task<int> CountAsync();
        void Delete(TEntity entity);
        void DeleteAll(IEnumerable<TEntity> entities);
        TEntity Find(Expression<Func<TEntity, bool>> match);
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        TEntity Get(TKey id);
        ICollection<TEntity> GetAll();
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TKey id);
        void Update(TEntity entity, TKey key);
        Task UpdateAsync(TEntity entity, TKey key);
        void SaveChanges();
        Task SaveChangesAsync();
        bool HasAny();
        Task<bool> HasAnyAsync();
        bool Exist(TKey id);
    }
}
