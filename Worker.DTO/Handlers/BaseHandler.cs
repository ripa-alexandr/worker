using System.Collections.Generic;

using Worker.DAL.Api;
using Worker.DTO.Api;
using Worker.DTO.Mapping;

namespace Worker.DTO.Handlers
{
    /// <summary>
    /// The base handler.
    /// </summary>
    /// <typeparam name="TDto">The Dto object.</typeparam>
    /// <typeparam name="TEntity">The entity object.</typeparam>
    public class BaseHandler<TDto, TEntity>
        where TDto : class, IBaseDto 
        where TEntity : class, IBaseEntity
    {
        /// <summary>
        /// The repository instance.
        /// </summary>
        protected readonly IRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseHandler"/> class.
        /// </summary>
        protected BaseHandler(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Get one object by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The <see cref="TDto"/>.</returns>
        public virtual TDto Get(int id)
        {
            var entity = repository.Get<TEntity>(id);

            return AppAutoMapper.Map<TDto>(entity);
        }

        /// <summary>
        /// The get all objects.
        /// </summary>
        /// <returns>The <see cref="IEnumerable"/>.</returns>
        public virtual IEnumerable<TDto> Get()
        {
            var entities = repository.Get<TEntity>();

            return AppAutoMapper.Map<IEnumerable<TDto>>(entities);
        }

        /// <summary>
        /// Create object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        public virtual void Create(TDto dto)
        {
            var entity = AppAutoMapper.Map<TEntity>(dto);

            repository.Add(entity);

            repository.Save();
        }

        /// <summary>
        /// Update object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        public virtual void Update(TDto dto)
        {
            var entity = repository.Get<TEntity>(dto.Id);

            AppAutoMapper.Map(dto, entity);

            repository.Save();
        }

        /// <summary>
        /// The delete object.
        /// </summary>
        /// <param name="id">The id.</param>
        public virtual void Delete(int id)
        {
            var entity = this.repository.Get<TEntity>(id);
            repository.Delete(entity);

            repository.Save();
        }
    }
}
