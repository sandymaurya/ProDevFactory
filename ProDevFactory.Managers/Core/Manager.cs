using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProDevFactory.Models.Entities;
using ProDevFactory.ORM.Core;

namespace ProDevFactory.Managers.Core
{
    public class Manager<TEntity> : IManager<TEntity>
        where TEntity : Entity
    {
        #region Properties

        /// <summary>
        /// Reference for UnitOfWorkPerHttpRequest
        /// </summary>
        protected UnitOfWork _unitOfWork { get { return UnitOfWork.UnitOfWorkPerHttpRequest; } }
        
        /// <summary>
        /// Reference for Context.DbSet Entities
        /// </summary>
        protected DbSet<TEntity> Entities
        {
            get { return _unitOfWork.Set<TEntity>(); }
        }

        #endregion Properties
        
        #region Synchronous Operations

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
        public virtual IQueryable<TEntity> QueryableEntities()
        {
            return Entities;
        }

        /// <summary>
        /// Filter the records and then return the result list
        /// </summary>
        /// <param name="filterExpression">Expression to filter the result</param>
        /// <returns>
        /// IList object of filtered records or null if expression is not satisfied by any records
        /// </returns>
        public virtual IList<TEntity> Where(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Entities.Where(filterExpression).ToList();
        }

        /// <summary>
        /// Return IList of entities for a page result, order by expression
        /// </summary>
        /// <typeparam name="TEntity"> Type of entity </typeparam>
        /// <typeparam name="TProperty"> Type of property used for ordering data </typeparam>
        /// <param name="pageIndex"> Index of page </param>
        /// <param name="pageCount"> Entity count per page</param>
        /// <param name="filterExpression"> Expression to filter the result </param>
        /// <param name="orderByExpression"> Expression to order the result based on a particular property </param>
        /// <param name="ascending"> Order of the list </param>
        /// <returns> IList of entites </returns>
        public virtual IList<TEntity> GetPage<TProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, bool>> filterExpression = null,
            Expression<Func<TEntity, TProperty>> orderByExpression = null, bool ascending = true)
        {
            var list = filterExpression != null ? Entities.Where(filterExpression) : Entities;
            list = orderByExpression != null ? list.OrderBy(orderByExpression) : list;

            if (ascending)
            {
                return list.Skip(pageCount * (pageIndex > 0 ? pageIndex - 1 : 0))
                          .Take(pageCount).ToList();
            }
            else
            {
                return list.Skip(pageCount * (pageIndex > 0 ? pageIndex - 1 : 0))
                          .Take(pageCount).ToList();
            }
        }


        /// <summary>
        /// Gets first record in database which satisfy the filter expression
        /// </summary>
        /// <param name="filterExpression">Expression to filter the result</param>
        /// <returns>
        /// Matching entity or null if expression is not satisfied by any record
        /// </returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Entities.FirstOrDefault(filterExpression);
        }

        /// <summary>
        /// Get an entity using is id field (id is the primary key of entity)
        /// </summary>
        /// <param name="id">primary key of the entity</param>
        /// <returns>
        /// Entity which matchs the primary key else null is returned
        /// </returns>
        public virtual TEntity GetById(int id)
        {
            return Entities.Find(id);
        }
        
        /// <summary>
        /// Set the EntityState to Added or Modified in database context
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>
        /// If entity Id (primary key) is 0 then state is set to Added else its set to Modified
        /// </remarks>
        public virtual void Save(TEntity entity)
        {
            if (entity.Id == 0)
            {
                Entities.Add(entity);
            }
            else
            {
                _unitOfWork.SetModified(entity);
            }
        }

        /// <summary>
        /// Set the EntityState of entity to Deleted in database context using its instance
        /// </summary>
        /// <param name="entity">Entity instance which need to be deleted</param>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null) return;
            Entities.Remove(entity);
        }

        /// <summary>
        /// Find and set the EntityState of entity to Deleted in database context using its primary key
        /// </summary>
        /// <param name="id"> Primary key of entity </param>
        public virtual void DeleteById(int id)
        {
            Delete(Entities.Find(id));
        }

        /// <summary>
        /// Counts the records in database if expression is supplied then counts the matching records in database
        /// </summary>
        /// <param name="filterExpression">Condition expression the match records</param>
        /// <returns>Count of recodrs matching the express if its not null else returns the count of all records</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            if (filterExpression != null)
            {
                return Entities.Count(filterExpression);
            }

            return Entities.Count();
        }
        
        #endregion Synchronous Operations

        #region Asynchronous Operations

        /// <summary>
        /// Filter the records and then returns the result list asynchronously
        /// </summary>
        /// <param name="filterExpression">Expression to filter the result</param>
        /// <returns>
        /// IList object of filtered records or null if expression is not satisfied by any records
        /// </returns>
        public virtual async Task<IList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await Entities.Where(filterExpression).ToListAsync();
        }

        /// <summary>
        /// Asynchronously returns IList of entities for a page result, order by expression
        /// </summary>
        /// <typeparam name="TEntity"> Type of entity </typeparam>
        /// <typeparam name="TProperty"> Type of property used for ordering data </typeparam>
        /// <param name="pageIndex"> Index of page </param>
        /// <param name="pageCount"> Entity count per page</param>
        /// <param name="filterExpression"> Expression to filter the result </param>
        /// <param name="orderByExpression"> Expression to order the result based on a particular property </param>
        /// <param name="ascending"> Order of the list </param>
        /// <returns> IList of entites </returns>
        public virtual async Task<IList<TEntity>> GetPageAsync<TProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, bool>> filterExpression = null,
            Expression<Func<TEntity, TProperty>> orderByExpression = null, bool ascending = true)
        {
            var list = filterExpression != null ? Entities.Where(filterExpression) : Entities;
            list = orderByExpression != null ? list.OrderBy(orderByExpression) : list;

            if (ascending)
            {
                return await list.Skip(pageCount * (pageIndex > 0 ? pageIndex - 1 : 0))
                          .Take(pageCount).ToListAsync();
            }
            else
            {
                return await list.Skip(pageCount * (pageIndex > 0 ? pageIndex - 1 : 0))
                          .Take(pageCount).ToListAsync();
            }
        }

        /// <summary>
        /// Asynchronously gets first record in database which satisfy the filter expression
        /// </summary>
        /// <param name="filterExpression">Expression to filter the result</param>
        /// <returns>
        /// Matching entity or null if expression is not satisfied by any record
        /// </returns>
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await Entities.FirstOrDefaultAsync(filterExpression);
        }

        /// <summary>
        /// Asynchronously get an entity using its primary key
        /// </summary>
        /// <param name="id">primary key of the entity</param>
        /// <returns>
        /// Entity which matchs the primary key else null is returned
        /// </returns>
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Asynchronously find and set the EntityState of entity to Deleted in database context using its primary key
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        public virtual async Task DeleteByIdAsync(int id)
        {
            var entity = await Entities.FindAsync(id);
            Delete(entity);
        }

        /// <summary>
        /// Asynchronouly counts the records in database 
        /// if expression is supplied then counts the matching records in database
        /// </summary>
        /// <param name="filterExpression">Condition expression the match records</param>
        /// <returns>Count of recodrs matching the express if its not null else returns the count of all records</returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            if (filterExpression != null)
            {
                return await Entities.CountAsync(filterExpression);
            }

            return await Entities.CountAsync();
        }

        #endregion Asynchronous Operations

        #region Commit Operations

        #region Synchronous Operations

        /// <summary>
        /// Set the EntityState to Added or Modified in database context and then save changes in database
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>
        /// If entity Id (primary key) is 0 then state is set to Added else its set to Modified
        /// </remarks>
        public virtual void SaveAndCommit(TEntity entity)
        {
            Save(entity);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Set the EntityState of entity to Deleted in database context using its instance 
        /// and then save changes in database
        /// </summary>
        /// <param name="entity">Entity instance which need to be deleted</param>
        public virtual void DeleteAndCommit(TEntity entity)
        {
            Delete(entity);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Find and set the EntityState of entity to Deleted in database context using its primary key
        /// and then save changes in database
        /// </summary>
        /// <param name="id"> Primary key of entity </param>
        public virtual void DeleteByIdAndCommit(int id)
        {
            DeleteById(id);
            _unitOfWork.SaveChanges();
        }

        #endregion Synchronous Operations

        #region Asynchronous Operations

        /// <summary>
        /// Set the EntityState to Added or Modified in database context 
        /// and then asynchronously save changes in database
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>
        /// If entity Id (primary key) is 0 then state is set to Added else its set to Modified
        /// </remarks>
        public virtual async Task SaveAndCommitAsync(TEntity entity)
        {
            Save(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Set the EntityState of entity to Deleted in database context using its instance 
        /// and then asynchronously save changes in database
        /// </summary>
        /// <param name="entity">Entity instance which need to be deleted</param>
        public virtual async Task DeleteAndCommitAsync(TEntity entity)
        {
            Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously find and set the EntityState of entity to Deleted in database context using its primary key
        /// and then asynchronously save changes in database
        /// </summary>
        /// <param name="id"> Primary key of entity </param>
        public virtual async Task DeleteByIdAndCommitAsync(int id)
        {
            await DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion Asynchronous Operations

        #endregion Commit Operations
    }
}
