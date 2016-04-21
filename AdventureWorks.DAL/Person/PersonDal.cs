using AdventureWorks.Model;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AdventureWorks.Dal
{
    public class PersonDal : ARepositoryT<PersonModel>, IPersonDal
    {
        #region static

        public static IDictionary<CredentialModel, DateTime> _logins = new System.Collections.Concurrent.ConcurrentDictionary<CredentialModel, DateTime>();

        public static bool ValidateUser(CredentialModel credential)
        {
            if (credential == null || String.IsNullOrWhiteSpace(credential.Login)) return false;

            if (!PersonDal._logins.ContainsKey(credential)) return false;

            if ((DateTime.Now - PersonDal._logins[credential]).TotalMinutes <= 20)
            {
                PersonDal._logins[credential] = DateTime.Now;
                return true;
            }

            PersonDal._logins.Remove(credential);
            return false;
        }

        #endregion

        #region contructor

        public PersonDal() : base() { }
        public PersonDal(string connectionString, string providerName) : base(connectionString, providerName) { }

        #endregion

        #region interface

        public override IList<PersonModel> SelectAll()
        {
            return this.SelectPerson(null);
        }

        public override PersonModel GetOneById(int id)
        {
            IList<PersonModel> person = SelectPerson(id);
            return person.FirstOrDefault();
        }

        public override PersonModel Insert(PersonModel item)
        {
            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_CreateNewPerson";

                    command.AddParameter("@p_type", item.Type.ToString());
                    command.AddParameter("@p_title", item.Title);
                    command.AddParameter("@p_firstName", item.FirstName);
                    command.AddParameter("@p_lastName", item.LastName);

                    IList<PersonModel> persons = this.ConvertDataTableToPersons(command);
                    if (persons == null || persons.Count != 1) return null;

                    item = persons.First();

                    return item;
                }
            }
        }

        public override bool Update(PersonModel item)
        {
            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_UpdatePerson";

                    command.AddParameter("@p_businessEntityID", DbType.Int32, item.Id);
                    command.AddParameter("@p_title", item.Title);
                    command.AddParameter("@p_firstName", item.FirstName);
                    command.AddParameter("@p_lastName", item.LastName);
                    command.AddParameter("@p_type", item.TypeString);

                    IList<PersonModel> persons = this.ConvertDataTableToPersons(command);

                    if (persons != null && persons.Count == 1) item = persons.First();

                    int result = persons.Count;

                    return (result == 1);

                }
            }
        }

        public override bool Delete(int id)
        {
            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_DeletePerson";

                    command.AddParameter("@p_businessEntity", DbType.Int32, id);

                    command.Connection.Open();

                    int result = command.ExecuteNonQuery();

                    return (result == 1);
                }
            }
        }

        #endregion

        public PersonModel GetOneByValidCredential(CredentialModel credential)
        {
            if (credential == null) throw new ArgumentNullException("credential");
            if (PersonDal._logins.ContainsKey(credential)) PersonDal._logins.Remove(credential);

            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_CheckPasswordValidity";

                    command.AddParameter("@p_emailAddress", credential.Login);
                    command.AddParameter("@p_password", credential.Password);

                    IList<PersonModel> persons = this.ConvertDataTableToPersons(command);
                    PersonModel person = persons.FirstOrDefault();

                    if (person != null) PersonDal._logins.Add(credential, DateTime.Now);

                    return person;
                }
            }
        }

        private IList<PersonModel> SelectPerson(int? id)
        {
            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_GetPersons";
                    command.AddParameter("@p_businessEntityID", DbType.Int32, id);

                    return this.ConvertDataTableToPersons(command);
                }
            }
        }

        private IList<PersonModel> ConvertDataTableToPersons(DbCommand command)
        {
            using (DbDataAdapter da = command.CreateDataAdapter())
            {
                using (DataTable dt = new DataTable() { Locale = CultureInfo.CurrentCulture, })
                {
                    da.Fill(dt);

                    IList<PersonModel> persons = (from DataRow row in dt.Rows
                                                  select new PersonModel()
                                                  {
                                                      Id = row.Field<int>("BusinessEntityID"),
                                                      Type = (PersonType)Enum.Parse(typeof(PersonType), row.Field<string>("PersonType"), true),
                                                      Title = row.Field<string>("Title"),
                                                      FirstName = row.Field<string>("FirstName"),
                                                      LastName = row.Field<string>("LastName"),
                                                      ModifiedDate = row.Field<DateTime>("ModifiedDate"),
                                                  }).ToList();

                    return persons;
                }
            }
        }
    }
}
