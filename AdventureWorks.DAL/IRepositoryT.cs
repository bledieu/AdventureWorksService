using System.Collections.Generic;

namespace AdventureWorks.Dal
{
    public interface IRepositoryT<T>  where T: class
    {
        IList<T> SelectAll();
        T GetOneById(int id);
        T Insert(T item);
        bool Delete(int id);
        bool Update(T item);
    }
}
