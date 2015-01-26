using System;
using System.Data.Entity;

namespace ProDevFactory.ORM.Contracts
{
    /// <summary>
    /// Base interface to implement UnitOfWork Pattern.
    /// </summary>
    public interface IUnitOfWork : IQueryableUnitOfWork, ISql, IDisposable
    {
        /// <summary>
        /// Start a new database transaction scope
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Use this in a using then you don't have to call Rollback
        /// </remarks>
        DbContextTransaction BeginTransaction();
    }
}
