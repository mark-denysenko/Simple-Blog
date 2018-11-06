using System;
using System.Collections.Generic;
using System.Linq;

namespace Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        //T Single(Func<T, bool> predicate);
        //T FirstOrDefault(Func<T, bool> predicate);
        //IQueryable<T> Where(Func<T, bool> predicate);
        //IQueryable<TResult> Select<TResult>(Func<T, TResult> selector);
        int Count();

        void Save();
    }
}
