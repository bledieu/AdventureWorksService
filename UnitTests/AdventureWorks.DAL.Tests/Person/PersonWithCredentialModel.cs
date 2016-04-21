using AdventureWorks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Dal.Tests
{
    public class PersonWithCredentialModel : PersonModel
    {
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
}
