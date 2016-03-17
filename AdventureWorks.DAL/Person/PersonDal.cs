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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private IList<PersonModel> SelectPerson(int? id)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("SELECT BusinessEntityID, Title, FirstName, MiddleName, LastName, ModifiedDate");
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
