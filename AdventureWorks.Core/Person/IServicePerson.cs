﻿using AdventureWorks.Model;
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
    public interface IServicePerson
    {
        [OperationContract]
        [WebGet(UriTemplate = "Persons/{personId}/", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PersonModel GetOne(string personId);

        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Persons/{personId}/")]
        void DelOne(string personId);

        [OperationContract]
        [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Persons/{personId}/")]
        void UpdateOne(PersonModel person, string personId);

        [OperationContract]
        [WebGet(UriTemplate = "Persons/", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        IList<PersonModel> GetAll();
    }
}
