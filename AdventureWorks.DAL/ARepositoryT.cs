using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Dal
{
    public abstract class ARepositoryT<T> : IRepositoryT<T> where T : class
    {
        private readonly string _connectionString;
        
        public ARepositoryT() : this(ConfigurationManager.ConnectionStrings["main"].ConnectionString) { }
        public ARepositoryT(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public abstract bool Delete(int id);
        public abstract T GetOneById(int id);
        public abstract bool Insert(T item);
        public abstract IList<T> SelectAll();
        public abstract bool Update(T item);
    }
}
