using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        private DbSet<T> _entities;
        public Repository(DbContext context)
        {
            this.context = context;
            this._entities=context.Set<T>();
        }
        public void Add(T entity)
        {
            this._entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            this._entities.AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this._entities.Where(predicate).ToList();
        }

        public T Get(int id)
        {
            return this._entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this._entities.ToList();
        }

        public void Remove(T entity)
        {
            this._entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            this._entities.RemoveRange(entities);
        }
    }
}
