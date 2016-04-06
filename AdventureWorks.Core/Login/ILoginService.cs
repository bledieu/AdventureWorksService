using AdventureWorks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Core.Login
{
    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "Login/", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PersonModel Login(CredentialModel credential);
    }
}
