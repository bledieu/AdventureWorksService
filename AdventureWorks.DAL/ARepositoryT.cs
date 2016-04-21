using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace AdventureWorks.Dal
{
    public abstract class ARepositoryT<T> : IRepositoryT<T> where T : class
    {
        private readonly string _connectionString;
        private readonly string _providerName;

        public ARepositoryT() {
            ConnectionStringSettings conSettings = ConfigurationManager.ConnectionStrings["main"];
            _connectionString = conSettings.ConnectionString;
            _providerName = conSettings.ProviderName;
        }
        public ARepositoryT(string connectionString, string providerName)
        {
            _connectionString = connectionString;
            _providerName = providerName;
        }

        public virtual bool Delete(int id) { throw new NotImplementedException(); }
        public virtual T GetOneById(int id) { throw new NotImplementedException(); }
        public virtual T Insert(T item) { throw new NotImplementedException(); }
        public virtual IList<T> SelectAll() { throw new NotImplementedException(); }
        public virtual bool Update(T item) { throw new NotImplementedException(); }

        protected internal DbConnection GetConnection()
        {
            
            //get the proper factory 
            DbProviderFactory factory = DbProviderFactories.GetFactory(_providerName);
            DbConnection con = factory.CreateConnection();
            con.ConnectionString = _connectionString;

            return con;
        }
    }
}
