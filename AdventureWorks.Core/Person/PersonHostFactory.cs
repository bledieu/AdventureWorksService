using BasicAuthenticationUsingWCF;
using Microsoft.ServiceModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;

namespace AdventureWorks.Core.Person
{
    public class PersonHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var serviceHost = new WebServiceHost2(serviceType, true, baseAddresses);
            serviceHost.Interceptors.Add(RequestInterceptorFactory.Create("DataWebService", new PersonProvider()));
            return serviceHost;
        }
    }
}