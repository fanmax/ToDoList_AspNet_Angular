using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Add(T obj);

        T GetById(int id);

        IEnumerable<T> GetAll();

        void Update(T obj);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Remove(int id);
    }
}
