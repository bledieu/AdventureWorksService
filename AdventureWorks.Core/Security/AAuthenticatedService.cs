using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Web;
//using OperationContext = System.ServiceModel.Web.MockedOperationContext;

namespace AdventureWorks.Core.Security
{
    public abstract class AAuthenticatedService
    {
        protected internal void ThrowNewFaultException_CoreDetailedException(string family, string description)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            string method = sf.GetMethod().Name;

            CoreDetailedException e = new CoreDetailedException(this.GetType().Name, method, family, description);

            throw new FaultException<CoreDetailedException>(e, description, new FaultCode(family));
        }
    }
}