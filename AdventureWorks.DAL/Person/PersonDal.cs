using AdventureWorks.Model.Person;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Dal
{
    public class PersonDal : ARepositoryT<PersonModel>
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
            IList<PersonModel> person = SelectPerson(id);
            return person.FirstOrDefault();
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
            return this.SelectPerson(null);
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

        private IList<PersonModel> SelectPerson(int? id)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("SELECT BusinessEntityID, PersonType, Title, FirstName, MiddleName, LastName, ModifiedDate");
            queryBuilder.AppendLine("FROM Person.Person");
            if (id != null) queryBuilder.AppendLine("WHERE BusinessEntityID = @id");

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    if (id != null) command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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
    }
}
