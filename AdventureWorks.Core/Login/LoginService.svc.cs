using AdventureWorks.Dal;
using AdventureWorks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AdventureWorks.Core.Login
{
    public class LoginService : ILoginService
    {
        public PersonModel Login(CredentialModel credential)
        {
            PersonDal person = new PersonDal();
            return person.GetOneByValidCredential(credential);
        }
    }
}
