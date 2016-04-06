using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace AdventureWorks.Core
{
    [DataContract]
    public class CoreDetailedException : CommunicationException
    {
        [DataMember]
        public string Class { get; set; }

        [DataMember]
        public string Method { get; set; }

        [DataMember]
        public string Family { get; set; }

        [DataMember]
        public string Reason { get; set; }

        public CoreDetailedException(string className, string method, string family, string description) : this(description)
        {
            Class = className;
            Method = method;
            Family = family;
            Reason = description;
        }
        public CoreDetailedException() : base() { }
        public CoreDetailedException(string message) : base(message) { }
        public CoreDetailedException(string message, Exception innerException) : base(message, innerException) { }
        public CoreDetailedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}