using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
