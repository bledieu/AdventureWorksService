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
            if (String.IsNullOrWhiteSpace(id)) this.ThrowNewFaultException_CoreDetailedException("NULL_ARGUMENT", "id is null");

            PersonModel person;

            try { person = _repository.GetOneById(Convert.ToInt32(id, CultureInfo.InvariantCulture)); }
            catch (Exception) { throw new FaultException("FAILED"); }

            if (person == null) this.ThrowNewFaultException_CoreDetailedException("UNKNOWN", String.Format("Unknown person with {0} id", id));

            return person;
        }

        public IList<PersonModel> GetAll()
        {
            try { return _repository.SelectAll(); }
            catch (Exception) { throw new FaultException("FAILED"); }
        }

        public void DeleteOne(string personId)
        {
            if (String.IsNullOrWhiteSpace(personId)) this.ThrowNewFaultException_CoreDetailedException("NULL_ARGUMENT", "id is null");

            bool result = false;

            try { result = _repository.Delete(Convert.ToInt32(personId)); }
            catch (Exception) { throw new FaultException("FAILED"); }

            if (!result) this.ThrowNewFaultException_CoreDetailedException("UNKNOWN", String.Format("Unable to delete person with {0} id", personId));
        }

        public void UpdateOne(PersonModel person, string personId)
        {
            if (person == null) this.ThrowNewFaultException_CoreDetailedException("NULL_ARGUMENT", "model is null");
            if (String.IsNullOrWhiteSpace(personId)) this.ThrowNewFaultException_CoreDetailedException("NULL_ARGUMENT", "id is null");

            if (Convert.ToInt32(personId) != person.Id) this.ThrowNewFaultException_CoreDetailedException("MISMATCH", String.Format("id parameter '{0}' and id model '{1}' doesn't match", personId, person.Id));

            bool result = false;
            try { result = _repository.Update(person); }
            catch (Exception) { throw new FaultException("FAILED"); }

            if (!result) this.ThrowNewFaultException_CoreDetailedException("UNKNOWN", String.Format("Unable to update person with {0} id", personId));
        }

        public PersonModel CreateOne(PersonModel newPerson)
        {
            if (newPerson == null) this.ThrowNewFaultException_CoreDetailedException("NULL_ARGUMENT", "model is null");

            PersonModel result = null;

            try { result = _repository.Insert(newPerson); }
            catch (Exception) { throw new FaultException("FAILED"); }

            if (result == null) this.ThrowNewFaultException_CoreDetailedException("UNKNOWN", "Unable to create a new person");
            return result;

        }
    }
}
