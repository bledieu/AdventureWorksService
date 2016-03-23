using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}