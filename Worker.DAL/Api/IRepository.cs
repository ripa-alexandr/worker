using System;
using System.Linq;
using System.Linq.Expressions;

namespace Worker.DAL.Api
{
    /// <summary>
    /// The Repository interface.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Get one entity by id.
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <param name="id">The id of entity.</param> 
        /// <returns>The entity.</returns>
        T Get<T>(int id) where T : class, IBaseEntity;

        /// <summary>
        /// Get all entities from database.
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <returns>The IQueryable of entity.</returns>
        IQueryable<T> Get<T>() where T : class, IBaseEntity;

        /// <summary>
        /// Get entities from database with filter 
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <param name="where">The filter.</param>
        /// <returns>The IQueryable of entity.</returns>
        IQueryable<T> Get<T>(Expression<Func<T, bool>> where) where T : class, IBaseEntity;

        /// <summary>
        /// Add entity to database
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <param name="data">The data.</param>
        void Add<T>(T data) where T : class, IBaseEntity;

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <typeparam name="T">The class of entity.</typeparam>
        /// <param name="id">The id of entity.</param>
        /// <param name="data"></param>
        void Delete<T>(T data) where T : class, IBaseEntity;

        /// <summary>
        /// Save changes in database
        /// </summary>
        void Save();
    }
}
