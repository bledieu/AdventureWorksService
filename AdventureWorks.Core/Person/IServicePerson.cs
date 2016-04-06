using AdventureWorks.Core.Core;
using AdventureWorks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AdventureWorks.Core.Person
{
    [ServiceContract]
    public interface IServicePerson : IService<PersonModel>
    {
        [OperationContract]
        [WebGet(UriTemplate = "Persons/", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        new IList<PersonModel> GetAll();

        [WebGet(UriTemplate = "Persons/{personId}/", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        [FaultContract(typeof(CoreDetailedException))]
        new PersonModel GetOne(string personId);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Persons/")]
        new PersonModel CreateOne(PersonModel person);

        [OperationContract]
        [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Persons/{personId}/")]
        new void UpdateOne(PersonModel person, string personId);

        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Persons/{personId}/")]
        new void DeleteOne(string personId);
    }
}
