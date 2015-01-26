using System.Linq;

namespace ProDevFactory.Managers.Core
{
    public interface ICommonManager<TEntity>
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>
        /// IQueryable list to filter and then process the result
        /// </returns>
        /// <remarks>
        /// Result of his method can be used to filter the all records 
        /// by various conditions without loading them in memory
        /// </remarks>
        IQueryable<TEntity> QueryableEntities();

        /// <summary>
        /// Set the EntityState to Added or Modified in database context
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>
        /// If entity Id (primary key) is 0 then state is set to Added else its set to Modified
        /// </remarks>
        void Save(TEntity entity);

        /// <summary>
        /// Set the EntityState of entity to Deleted in database context using its instance
        /// </summary>
        /// <param name="entity">Entity instance which need to be deleted</param>
        void Delete(TEntity entity);
    }
}
