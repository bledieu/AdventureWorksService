using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Core.Core
{
    public interface IService<T> where T: class
    {
        T GetOne(string id);

        T CreateOne(T data);

        void UpdateOne(T data, string id);

        void DeleteOne(string id);
        
        IList<T> GetAll();
    }
}
