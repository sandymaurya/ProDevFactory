using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProDevFactory.Managers.Core
{
    public interface ISynchronousManager<TEntity> : ICommonManager<TEntity>
    {
        /// <summary>
        /// Filter the records and then return the result list
        /// </summary>
        /// <param name="filterExpression">Expression to filter the result</param>
        /// <returns>
        /// IList object of filtered records or null if expression is not satisfied by any records
        /// </returns>
        IList<TEntity> Where(Expression<Func<TEntity, bool>> filterExpression);


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
        IList<TEntity> GetPage<TProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> filterExpression = null, Expression<Func<TEntity, TProperty>> orderByExpression = null, bool ascending = true);

        /// <summary>
        /// Gets first record in database which satisfy the filter expression
        /// </summary>
        /// <param name="filterExpression">Expression to filter the result</param>
        /// <returns>
        /// Matching entity or null if expression is not satisfied by any record
        /// </returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Get an entity using is id field (id is the primary key of entity)
        /// </summary>
        /// <param name="id">primary key of the entity</param>
        /// <returns>
        /// Entity which matchs the primary key else null is returned
        /// </returns>
        TEntity GetById(int id);

        /// <summary>
        /// Find and set the EntityState of entity to Deleted in database context using its primary key
        /// </summary>
        /// <param name="id"> Primary key of entity </param>
        void DeleteById(int id);

        /// <summary>
        /// Counts the records in database if expression is supplied then counts the matching records in database
        /// </summary>
        /// <param name="filterExpression">Condition expression the match records</param>
        /// <returns>Count of recodrs matching the express if its not null else returns the count of all records</returns>
        int Count(Expression<Func<TEntity, bool>> filterExpression = null);


        #region Commit Operations

        /// <summary>
        /// Set the EntityState to Added or Modified in database context and then save changes in database
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>
        /// If entity Id (primary key) is 0 then state is set to Added else its set to Modified
        /// </remarks>
        void SaveAndCommit(TEntity entity);

        /// <summary>
        /// Set the EntityState of entity to Deleted in database context using its instance 
        /// and then save changes in database
        /// </summary>
        /// <param name="entity">Entity instance which need to be deleted</param>
        void DeleteAndCommit(TEntity entity);

        /// <summary>
        /// Find and set the EntityState of entity to Deleted in database context using its primary key
        /// and then save changes in database
        /// </summary>
        /// <param name="id"> Primary key of entity </param>
        void DeleteByIdAndCommit(int id);

        #endregion Commit Operations
    }
}
