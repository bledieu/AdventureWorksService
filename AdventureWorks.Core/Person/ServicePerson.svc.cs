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
        private IRepositoryT<PersonModel> _repository;

        public ServicePerson() : this(new PersonDal()) { }
        public ServicePerson(IRepositoryT<PersonModel> repository)
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

        public bool AddOne(PersonModel newPerson)
        {
            _repository.Insert(newPerson);
            return true;
        }
    }
}
