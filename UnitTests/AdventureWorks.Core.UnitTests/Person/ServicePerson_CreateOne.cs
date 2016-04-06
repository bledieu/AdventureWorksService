using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AdventureWorks.Core.Person;
using AdventureWorks.Model;
using System.Collections.Generic;
using AdventureWorks.Dal;
using AdventureWorks.Dal.Tests;
using System.Linq;
using OperationContext = System.ServiceModel.Web.MockedOperationContext;
using System.ServiceModel;
using System.ServiceModel.Web;
using AdventureWorks.Core;

namespace AdventureWorks.UnitTests.Person
{
    [TestClass]
    public class ServicePerson_CreateOne : AServicePersonTest
    {
        [TestMethod]
        public void CreateAPersonPersitsInDb()
        {
            //arrange
            PersonModel newPerson = new PersonModel(0, PersonType.GC, "Mr.", "John", "Smith");
            ServicePerson controller = new ServicePerson(_repository);

            int personsCount = _repository.SelectAll().Count();

            //act
            newPerson = controller.CreateOne(newPerson);

            //assert
            Assert.AreEqual(personsCount + 1, _repository.SelectAll().Count());
            Assert.IsNotNull(newPerson);
            Assert.AreNotEqual(0, newPerson.Id);
            _mockedRepository.Verify(x => x.Insert(It.IsAny<PersonModel>()), Times.Once);
        }
        
        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void CreateANullPersonThrowException()
        {
            ServicePerson controller = new ServicePerson(_repository);
            try { controller.CreateOne(null); }
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("CreateOne", e.Detail.Method);
                Assert.AreEqual("NULL_ARGUMENT", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ExceptionDuringDalThrowException()
        {
            _mockedRepository.Setup(r => r.Insert(It.IsAny<PersonModel>())).Returns(
                (PersonModel target) =>
                {
                    throw new Exception();
                });

            PersonModel newPerson = new PersonModel(0, PersonType.GC, "Mr.", "John", "Smith");
            ServicePerson controller = new ServicePerson(_repository);
            try { controller.CreateOne(newPerson); }
            catch (FaultException e)
            {
                Assert.AreEqual("FAILED", e.Message);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void NullReturnFromDalThrowException()
        {
            _mockedRepository.Setup(r => r.Insert(It.IsAny<PersonModel>())).Returns(
                (PersonModel target) =>
                {
                    return null;
                });

            PersonModel newPerson = new PersonModel(0, PersonType.GC, "Mr.", "John", "Smith");
            ServicePerson controller = new ServicePerson(_repository);
            try { controller.CreateOne(newPerson); }
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("CreateOne", e.Detail.Method);
                Assert.AreEqual("UNKNOWN", e.Detail.Family);

                throw e;
            }
        }

    }
}
