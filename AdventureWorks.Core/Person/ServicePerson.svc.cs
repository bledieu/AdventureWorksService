using AdventureWorks.Dal;
using AdventureWorks.Model;
using AdventureWorks.Core.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AdventureWorks.Core.Person
{
    public sealed class ServicePerson : AAuthenticatedService, IServicePerson
    {
        private IPersonDal _repository;

        public ServicePerson() : this(new PersonDal()) { }
        public ServicePerson(IPersonDal repository)
        {
            _repository = repository;
        }

        public PersonModel GetOne(string id)
        {
            var person = _repository.GetOneById(Convert.ToInt32(id, CultureInfo.InvariantCulture));
            return person;
        }

        public IList<PersonModel> GetAll()
        {
            var persons = _repository.SelectAll();
            return persons;
        }

        public void DeleteOne(string personId)
        {
            int id;
            if (Int32.TryParse(personId, out id)) _repository.Delete(id);
        }

        public void UpdateOne(PersonModel person, string personId)
        {
            int id;
            if (Int32.TryParse(personId, out id)) _repository.Update(person);
        }

        public PersonModel CreateOne(PersonModel newPerson)
        {
           return _repository.Insert(newPerson);
        }
    }
}
