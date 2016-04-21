using System;
using System.Runtime.Serialization;

namespace AdventureWorks.Model
{
    [DataContract]
    public sealed class CredentialModel : IEquatable<CredentialModel>
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }


        public override bool Equals(object obj)
        {
            return Equals(obj as CredentialModel);
        }
        public override int GetHashCode()
        {
            int hash = Login.GetHashCode();
            hash ^= Password.GetHashCode();
            return hash;
        }
        public bool Equals(CredentialModel other)
        {
            if (other == null) return false;
            return GetHashCode() == other.GetHashCode();
        }
    }
}
