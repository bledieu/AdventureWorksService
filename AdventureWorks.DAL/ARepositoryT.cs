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

        public virtual bool Delete(int id) { throw new NotImplementedException(); }
        public virtual T GetOneById(int id) { throw new NotImplementedException(); }
        public virtual bool Insert(T item) { throw new NotImplementedException(); }
        public virtual IList<T> SelectAll() { throw new NotImplementedException(); }
        public virtual bool Update(T item) { throw new NotImplementedException(); }
    }
}
