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
        protected internal bool CheckIfUserIsAuthenticated()
        {
            if (OperationContext.Current == null) return true;

            ServiceSecurityContext securityCtx = OperationContext.Current.ServiceSecurityContext;

            if ((securityCtx.PrimaryIdentity.IsAuthenticated != true) || (securityCtx.PrimaryIdentity.Name != "Team_Project"))
            {
                throw new UnauthorizedAccessException("You are not permitted to call this method. Access Denied.");
            }

            return true;
        }

        protected internal void ThrowNewFaultException_CoreDetailedException(string family, string description)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            string method = sf.GetMethod().Name;

            CoreDetailedException e = new CoreDetailedException(this.GetType().Name, method, family, description);

            //using (MemoryStream stream = new MemoryStream())
            //{
            //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(CoreDetailedException));
            //    ser.WriteObject(stream, e);
            //    stream.Position = 0;
            //    using (StreamReader sr = new StreamReader(stream)) description = sr.ReadToEnd();
            //}

            throw new FaultException<CoreDetailedException>(e, description, new FaultCode(family));
        }
    }
}