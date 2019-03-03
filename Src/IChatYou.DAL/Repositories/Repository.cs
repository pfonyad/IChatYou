namespace IChatYou.DAL.Repositories
{
    using DAL;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected ApplicationDbContext Context;
        protected DbSet<TEntity> DbSet;

        /// <summary>
        /// The contructor requires an open DataContext to work with
        /// </summary>
        /// <param name="context">An open DataContext</param>
        protected Repository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }
        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public TEntity Get(TKey id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public async Task<TEntity> GetAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }
        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>An ICollection of every object in the database</returns>
        public ICollection<TEntity> GetAll()
        {
            return DbSet.ToList();
        }
        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>An ICollection of every object in the database</returns>
        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }
        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="match">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter. 
        /// If more than one object is found or if zero are found, null is returned</returns>
        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return DbSet.SingleOrDefault(match);
        }
        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="match">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter. 
        /// If more than one object is found or if zero are found, null is returned</returns>
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await DbSet.SingleOrDefaultAsync(match);
        }
        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="match">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return DbSet.Where(match).ToList();
        }
        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="match">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await DbSet.Where(match).ToListAsync();
        }
        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="entity">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void AddOrUpdate(Expression<Func<TEntity, object>> identifierExpression, params TEntity[] entities)
        {
            DbSet.AddOrUpdate(identifierExpression, entities);
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="list">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        public void AddAll(IEnumerable<TEntity> list)
        {
            DbSet.AddRange(list);
        }
        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="entity">The updated object to apply to the database</param>
        /// <param name="key">The primary key of the object to update</param>
        /// <returns>The resulting updated object</returns>
        public void Update(TEntity entity, TKey key)
        {
            if (entity == null)
                return;

            var existing = DbSet.Find(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(entity);
            }
        }
        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="entity">The updated object to apply to the database</param>
        /// <param name="key">The primary key of the object to update</param>
        /// <returns>The resulting updated object</returns>
        public async Task UpdateAsync(TEntity entity, TKey key)
        {
            if (entity == null)
                return;

            var existing = await DbSet.FindAsync(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(entity);
            }
        }
        /// <summary>
        /// Deletes a single object from the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="entity">The object to delete</param>
        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }
        /// <summary>
        /// Deletes objects from the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="entities">The objects to delete</param>
        public void DeleteAll(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Gets the count of the number of objects in the databse
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>The count of the number of objects</returns>
        public int Count()
        {
            return DbSet.Count();
        }
        /// <summary>
        /// Gets the count of the number of objects in the databse
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>The count of the number of objects</returns>
        public async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public bool HasAny()
        {
            return DbSet.Any();
        }

        public async Task<bool> HasAnyAsync()
        {
            return await DbSet.AnyAsync();
        }

        public bool Exist(TKey id)
        {
            return DbSet.Find(id) != null;
        }
    }
}
