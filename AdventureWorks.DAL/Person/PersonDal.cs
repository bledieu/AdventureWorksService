using AdventureWorks.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Dal
{
    public class PersonDal : ARepositoryT<PersonModel>, IPersonDal
    {
        public PersonDal() : base() { }
        public PersonDal(string connectionString) : base(connectionString) { }

        public override bool Delete(int id)
        {
            string sqlQueryMail = "DELETE FROM Person.EmailAddress WHERE BusinessEntityID=@id";
            string sqlQueryPerson = "DELETE FROM Person.Person WHERE BusinessEntityID=@id";

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQueryMail, connection))
                {
                    command.Connection.Open();
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.ExecuteNonQuery();

                    command.CommandText = sqlQueryPerson;
                    int result = command.ExecuteNonQuery();
                    return (result == 1);
                }
            }
        }

        public override PersonModel GetOneById(int id)
        {
            IList<PersonModel> person = SelectPerson(id, null);
            return person.FirstOrDefault();
        }

        public static IDictionary<string, LoginModel> _logins = new System.Collections.Concurrent.ConcurrentDictionary<string, LoginModel>();

        public static bool ValidateUser(string username)
        {
            if (String.IsNullOrWhiteSpace(username)) return false;

            if (!PersonDal._logins.ContainsKey(username)) return false;

            LoginModel login = PersonDal._logins[username];
            if ((DateTime.Now - login.LastLogonAt).TotalMinutes <= 20)
            {
                login.LastLogonAt = DateTime.Now;
                return true;
        }

            PersonDal._logins.Remove(username);
            return false;
        }

        private bool ValidatePassword(CredentialModel credential)
        {
            LoginModel login = SelectPassword(credential.Login);
            if (login == null) return false;

            byte[] passwordBytes = System.Text.Encoding.Unicode.GetBytes(credential.Password);
            byte[] saltBytes = Convert.FromBase64String(login.PasswordSalt);

            passwordBytes = passwordBytes.Concat(saltBytes).ToArray();

            string sha512 = Convert.ToBase64String(System.Security.Cryptography.HashAlgorithm.Create("SHA512").ComputeHash(passwordBytes));
            UpdatePassword(credential.Login, sha512);

            if (login.PasswordHash != sha512) return false;
            PersonDal._logins.Add(credential.Login, login);

            return true;
        }

        public PersonModel GetOneByValidCredential(CredentialModel credential)
        {
            if (credential == null) throw new ArgumentNullException("credential");

            IList<PersonModel> persons = SelectPerson(null, credential.Login);
            PersonModel person = persons.FirstOrDefault();

            if (person == null) return null;

            if (PersonDal.ValidateUser(credential.Login)) return person;
            if (!ValidatePassword(credential)) return null;

            return person;
        }

        public override bool Insert(PersonModel item)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("INSERT INTO Person.Person(BusinessEntityID, PersonType, Title, FirstName, LastName, ModifiedDate)");
            queryBuilder.AppendLine("VALUES(@id, @type, @title, @firstName, @lastName, @modifiedDate)");

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
                    command.Parameters.Add("@type", SqlDbType.VarChar).Value = item.Type.ToString();
                    command.Parameters.Add("@title", SqlDbType.VarChar).Value = item.Title;
                    command.Parameters.Add("@firstName", SqlDbType.VarChar).Value = item.FirstName;
                    command.Parameters.Add("@lastName", SqlDbType.VarChar).Value = item.LastName;
                    command.Parameters.Add("@modifiedDate", SqlDbType.DateTime).Value = DateTime.Now;

                    int result = command.ExecuteNonQuery();

                    return (result == 1);
                }
            }
        }

        public override IList<PersonModel> SelectAll()
        {
            return this.SelectPerson(null, null);
        }

        private IList<PersonModel> SelectPerson(int? id, string email)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("SELECT Per.BusinessEntityID, Per.PersonType, Per.Title, Per.FirstName, Per.MiddleName, Per.LastName, Per.ModifiedDate");
            queryBuilder.AppendLine("FROM Person.Person Per");
            queryBuilder.AppendLine("   INNER JOIN Person.EmailAddress Mail ON Mail.BusinessEntityID = Per.BusinessEntityID");

            IList<string> where = new Collection<string>();
            if (id != null) where.Add("Per.BusinessEntityID = @id");
            if (!String.IsNullOrWhiteSpace(email)) where.Add("Mail.EmailAddress = @email");
            if (where.Count > 0) queryBuilder.AppendLine(String.Format("WHERE {0};", String.Join(" AND ", where)));

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    if (id != null) command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    if (!String.IsNullOrWhiteSpace(email)) command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;

                    using (DataTable dt = new DataTable() { Locale = CultureInfo.CurrentCulture, })
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(dt);

                            IList<PersonModel> persons = (from DataRow row in dt.Rows
                                                          select new PersonModel(row.Field<int>("BusinessEntityID"),
                                                                                 (PersonType)Enum.Parse(typeof(PersonType), row.Field<string>("PersonType"), true),
                                                                                 row.Field<string>("Title"),
                                                                                 row.Field<string>("FirstName"),
                                                                                 row.Field<string>("LastName"))
                                                          {
                                                              ModifiedDate = row.Field<DateTime>("ModifiedDate"),
                                                          }).ToList();

                            return persons;
                        }
                    }
                }
            }
        }

        public override bool Update(PersonModel item)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("UPDATE Person.Person");
            queryBuilder.AppendLine("SET Title=@Title, FirstName=@FirstName, LastName=@LastName, PersonType=@PersonType, ModifiedDate=@ModifiedDate");
            queryBuilder.AppendLine("WHERE BusinessEntityID = @id");

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
                    command.Parameters.Add("@Title", SqlDbType.VarChar).Value = (item.Title != null) ? item.Title : (object)DBNull.Value;
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = item.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = item.LastName;
                    command.Parameters.Add("@PersonType", SqlDbType.NChar).Value = item.TypeString;
                    command.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = item.ModifiedDate;

                    command.Connection.Open();
                    int result = command.ExecuteNonQuery();
                    return (result == 1);

                }
            }
        }

        public LoginModel SelectPassword(string email)
        {
            if (String.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("SELECT Pass.PasswordSalt, Pass.PasswordHash");
            queryBuilder.AppendLine("FROM Person.Password Pass");
            queryBuilder.AppendLine("   INNER JOIN Person.EmailAddress Mail ON Mail.BusinessEntityID = Pass.BusinessEntityID");
            queryBuilder.AppendLine("WHERE Mail.EmailAddress = @email");

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;

                    using (DataTable dt = new DataTable() { Locale = CultureInfo.CurrentCulture, })
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(dt);

                            if (dt == null || dt.Rows.Count == 0) return null;

                            DataRow row = dt.Rows[0];
                            return new LoginModel()
                            {
                                Email = email,
                                PasswordSalt = row.Field<string>("PasswordSalt"),
                                PasswordHash = row.Field<string>("PasswordHash"),
                                LastLogonAt = DateTime.Now,
                            };
                        }
                    }
                }
            }
        }
        public void UpdatePassword(string email, string password)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("UPDATE Pass SET PasswordHash = @hash");
            queryBuilder.AppendLine("FROM Person.Password Pass");
            queryBuilder.AppendLine("   INNER JOIN Person.EmailAddress Mail ON Mail.BusinessEntityID = Pass.BusinessEntityID");
            queryBuilder.AppendLine("WHERE Mail.EmailAddress = @email");
            
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                    command.Parameters.Add("@hash", SqlDbType.VarChar).Value = password;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
