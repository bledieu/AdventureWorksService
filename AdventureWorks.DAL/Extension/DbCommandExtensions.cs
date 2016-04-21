using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Dal
{
    public static class DbCommandExtensions
    {
        /// <summary>
        /// Add a new parameter to the current dbCommand
        /// </summary>
        /// <param name="fieldName">Parameter name</param>
        /// <param name="fieldValue">Parameter value</param>
        public static void AddParameter(this DbCommand dbCommand, string fieldName, object fieldValue)
        {
            dbCommand.AddParameter(fieldName, DbType.String, fieldValue);
        }

        /// <summary>
        /// Add a new parameter to the current dbCommand
        /// </summary>
        /// <param name="fieldName">Parameter name</param>
        /// <param name="fieldType">Parameter type (String by default)</param>
        /// <param name="fieldValue">Parameter value</param>
        public static void AddParameter(this DbCommand dbCommand, string fieldName, DbType fieldType, object fieldValue)
        {
            DbParameter dbParameter = dbCommand.CreateParameter();
            dbParameter.ParameterName = fieldName;
            dbParameter.DbType = fieldType;
            dbParameter.Value = (fieldValue ?? DBNull.Value);

            dbCommand.Parameters.Add(dbParameter);
        }

        public static DbDataAdapter CreateDataAdapter(this DbCommand dbCommand)
        {
            DbDataAdapter adapter;

            string name_space = dbCommand.Connection.GetType().Namespace;
            DbProviderFactory factory = DbProviderFactories.GetFactory(name_space);
            adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = dbCommand;

            return adapter;
        }
    }
}
