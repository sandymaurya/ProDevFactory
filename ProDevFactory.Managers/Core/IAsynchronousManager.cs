using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProDevFactory.Managers.Core
{
    public interface IAsynchronousManager<TEntity> : ICommonManager<TEntity>
    {
        #region Asynchronous Operations

        /// <summary>
        /// Filter the records and then returns the result list asynchronously
        /// </summary>
        /// <param name="filterExpression">Expression to filter the result</param>
        /// <returns>
        /// IList object of filtered records or null if expression is not satisfied by any records
        /// </returns>
        Task<IList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// IList of entities for a page result, order by expression
        /// </summary>
        /// <typeparam name="TEntity"> Type of entity </typeparam>
        /// <typeparam name="TProperty"> Type of property used for ordering data </typeparam>
        /// <param name="pageIndex"> Index of page </param>
        /// <param name="pageCount"> Entity count per page</param>
        /// <param name="filterExpression"> Expression to filter the result </param>
        /// <param name="orderByExpression"> Expression to order the result based on a particular property </param>
        /// <param name="ascending"> Order of the list </param>
        /// <returns> IList of entites </returns>
        Task<IList<TEntity>> GetPageAsync<TProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> filterExpression = null, Expression<Func<TEntity, TProperty>> orderByExpression = null, bool ascending = true);


        /// <summary>
        /// Asynchronously gets first record in database which satisfy the filter expression
        /// </summary>
        /// <param name="filterExpression">Expression to filter the result</param>
        /// <returns>
        /// Matching entity or null if expression is not satisfied by any record
        /// </returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Asynchronously get an entity using its primary key
        /// </summary>
        /// <param name="id">primary key of the entity</param>
        /// <returns>
        /// Entity which matchs the primary key else null is returned
        /// </returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously find and set the EntityState of entity to Deleted in database context using its primary key
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        Task DeleteByIdAsync(int id);

        /// <summary>
        /// Asynchronouly counts the records in database 
        /// if expression is supplied then counts the matching records in database
        /// </summary>
        /// <param name="filterExpression">Condition expression the match records</param>
        /// <returns>Count of recodrs matching the express if its not null else returns the count of all records</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filterExpression = null);
        
        #region Commit Operations

        /// <summary>
        /// Set the EntityState to Added or Modified in database context 
        /// and then asynchronously save changes in database
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>
        /// If entity Id (primary key) is 0 then state is set to Added else its set to Modified
        /// </remarks>
        Task SaveAndCommitAsync(TEntity entity);

        /// <summary>
        /// Set the EntityState of entity to Deleted in database context using its instance 
        /// and then asynchronously save changes in database
        /// </summary>
        /// <param name="entity">Entity instance which need to be deleted</param>
        Task DeleteAndCommitAsync(TEntity entity);

        /// <summary>
        /// Asynchronously find and set the EntityState of entity to Deleted in database context using its primary key
        /// and then asynchronously save changes in database
        /// </summary>
        /// <param name="id"> Primary key of entity </param>
        Task DeleteByIdAndCommitAsync(int id);
        
        #endregion Commit Operations

        #endregion Asynchronous Operations
    }
}
