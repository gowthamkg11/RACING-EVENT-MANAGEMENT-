using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.DAL.Reporsitory
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Private member variables...         
        internal TARDBContext Context;
        internal DbSet<TEntity> DbSet;
        #endregion
        #region Public Constructor...         
        /// <summary>
        ///  Public Constructor,initializes privately declared local variables. 
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(TARDBContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();

        }
        #endregion

        #region Public member methods... 
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).ToList();
        }
        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).AsQueryable();
        }

        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }
        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }
        public IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }
        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }
        public TEntity GetSingle(Func<TEntity, bool> predicate) { return DbSet.Single<TEntity>(predicate); }
        /// <summary>         
        /// The first record matching the specified criteria        
        /// </summary>         
        /// <param name="predicate">Criteria to match on</param>        
        /// <returns>A single record containing the first record matching the specified criteria</returns>         
        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return DbSet.First<TEntity>(predicate);
        }
        public IEnumerable<TEntity> QueryObjectGraph(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter, string children)

        {

            return DbSet.Include(children).Where(filter);

        }
        #endregion


    }
}
