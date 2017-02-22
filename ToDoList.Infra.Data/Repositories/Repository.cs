using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Infra.Data.Context;

namespace ToDoList.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected TodoListContext Db;
        protected DbSet<T> DbSet;

        public Repository(TodoListContext context)
        {
            Db = context;
            DbSet = Db.Set<T>();
        }

        public void Add(T obj)
        {
            DbSet.Add(obj);
        }                

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public void Update(T obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }       

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        
    }
}
