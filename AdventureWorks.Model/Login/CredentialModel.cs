using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Model
{
    [DataContract]
    public sealed class CredentialModel
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
