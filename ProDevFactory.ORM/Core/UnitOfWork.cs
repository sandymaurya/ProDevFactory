using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using ProDevFactory.Models.Entities;
using ProDevFactory.Models.Identity;
using ProDevFactory.ORM.Contracts;
using ProDevFactory.Repository.Configuration;

namespace ProDevFactory.ORM.Core
{
    public class UnitOfWork : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        #region Constructor and Create

        private const string MyUnitOfWorkPerRequestContext = "MUOWPRC";

        public UnitOfWork()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public static UnitOfWork UnitOfWorkPerHttpRequest
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(MyUnitOfWorkPerRequestContext))
                {
                    HttpContext.Current.Items.Add(MyUnitOfWorkPerRequestContext, new UnitOfWork());
                }
                return HttpContext.Current.Items[MyUnitOfWorkPerRequestContext] as UnitOfWork;
            }
        }
        public static UnitOfWork GetUnitOfWorkPerHttpRequest()
        {
            return UnitOfWorkPerHttpRequest;
        }

        public static UnitOfWork CreateNewContext()
        {
            return new UnitOfWork();
        }

        #endregion Constructor and Create

        #region DbSet

        private IDbSet<Student> _student;
        public IDbSet<Student> Students
        {
            get { return _student ?? (_student = base.Set<Student>()); }
        }

        #endregion DbSet

        #region IUnitOfWork Members

        public DbContextTransaction BeginTransaction()
        {
            return base.Database.BeginTransaction();
        }

        #endregion IUnitOfWork Members

        #region IQueryableUnitOfWork Members

        public void SetModified<T>(T item)
            where T : Entity
        {
            //this operation also attach item in object state manager
            base.Entry(item).State = EntityState.Modified;
        }
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : Entity
        {
            //if it is not attached, attach original and set current values
            base.Entry(original).CurrentValues.SetValues(current);
        }

        #endregion IQueryableUnitOfWork Members

        #region ISql Members

        public IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<T>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        public async Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters)
        {
            return await base.Database.ExecuteSqlCommandAsync(sqlCommand, parameters);
        }

        #endregion ISql Members

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove unused conventions
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new StudentConfiguration());
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Called automatically on Application_EndRequest()
        /// </summary>
        public static void DisposeUnitOfWork()
        {
            // Getting dbContext directly to avoid creating it in case it was not already created.
            var entityContext = HttpContext.Current.Items[MyUnitOfWorkPerRequestContext] as UnitOfWork;
            if (entityContext != null)
            {
                entityContext.Dispose();
                HttpContext.Current.Items.Remove(MyUnitOfWorkPerRequestContext);
            }
        }
        #endregion
    }
}

