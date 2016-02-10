using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Worker.DAL.Api;

namespace Worker.DAL.Repositories
{
    /// <summary>
    /// The repository.
    /// </summary>
    public class Repository : IRepository
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly DbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get one entity by id.
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <param name="id">The id of entity.</param> 
        /// <returns>The entity.</returns>
        public T Get<T>(int id) where T : class, IBaseEntity
        {
            return this.Get<T>().FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// Get all entities from database.
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <returns>The <see cref="IQueryable"/>.</returns>
        public IQueryable<T> Get<T>() where T : class, IBaseEntity
        {
            return this.Get<T>(i => true);
        }

        /// <summary>
        /// Get entities from database with filter 
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <param name="where">The filter.</param>
        /// <returns>The <see cref="IQueryable"/>.</returns>
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> where) where T : class, IBaseEntity
        {
            return this.context.Set<T>().Where(where);
        }

        /// <summary>
        /// Add entity to database
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <param name="data">The data.</param>
        public void Add<T>(T data) where T : class, IBaseEntity
        {
            this.context.Set<T>().Add(data);
        }

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <param name="id">The id of entity.</param>
        /// <param name="data"></param>
        public void Delete<T>(T data) where T : class, IBaseEntity
        {
            this.context.Set<T>().Remove(data);
        }

        /// <summary>
        /// Save changes in database
        /// </summary>
        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
