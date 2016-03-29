using AdventureWorks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Dal
{
    public interface IPersonDal : IRepositoryT<PersonModel>
    {
        LoginModel SelectPassword(string email);
        void UpdatePassword(string email, string password);
    }
}
