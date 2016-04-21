using AdventureWorks.Core;
using AdventureWorks.Core.Person;
using AdventureWorks.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.ServiceModel;

namespace AdventureWorks.UnitTests.Person
{
    [TestClass]
    public class ServicePerson_UpdateOne : AServicePersonTest
    {
        private PersonModel _person;

        public ServicePerson_UpdateOne()
        {
            ServicePerson controller = new ServicePerson(_repository);
            _person = controller.GetOne("2");
        }

        [TestMethod]
        public void UpdatePersonSuccessfullySetProperties()
        {
            //arrange
            DateTime modifiedDate = _person.ModifiedDate;
            PersonModel person = new PersonModel() { Id = 2, Type = PersonType.IN, Title = "M.", FirstName = "Ronald", LastName = "McDonald", };
            ServicePerson controller = new ServicePerson(_repository);

            //act
            controller.UpdateOne(person, "2");

            //assert
            PersonModel personRecovered = controller.GetOne("2");

            Assert.IsNotNull(personRecovered);
            Assert.AreEqual(2, personRecovered.Id);
            Assert.AreEqual(PersonType.IN, personRecovered.Type);
            Assert.AreEqual("IN", personRecovered.TypeString);
            Assert.AreEqual("M.", personRecovered.Title);
            Assert.AreEqual("Ronald", personRecovered.FirstName);
            Assert.AreEqual("McDonald", personRecovered.LastName);
            Assert.IsTrue(personRecovered.ModifiedDate > modifiedDate);
            _mockedRepository.Verify(x => x.Update(It.IsAny<PersonModel>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void UpdateANullPersonThrowException()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.UpdateOne(null, "1"); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("UpdateOne", e.Detail.Method);
                Assert.AreEqual("NULL_ARGUMENT", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void UpdateWithANullIdThrowException()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.UpdateOne(_person, ""); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("UpdateOne", e.Detail.Method);
                Assert.AreEqual("NULL_ARGUMENT", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void UpdateWithAnIdDifferentFromModelThrowException()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.UpdateOne(_person, "10"); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("UpdateOne", e.Detail.Method);
                Assert.AreEqual("MISMATCH", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void UpdateWithABadIdThrowException()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);
            PersonModel person = new PersonModel() { Id = 250, Type = PersonType.IN, Title = "M.", FirstName = "Ronald", LastName = "McDonald", };

            //act
            try { controller.UpdateOne(person, "250"); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("UpdateOne", e.Detail.Method);
                Assert.AreEqual("UNKNOWN", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void FalseReturnFromDalThrowException()
        {
            //arrange
            _mockedRepository.Setup(r => r.Update(It.IsAny<PersonModel>())).Returns((PersonModel p) => { return false; });
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.UpdateOne(_person, "2"); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("UpdateOne", e.Detail.Method);
                Assert.AreEqual("UNKNOWN", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ExceptionFromDalThrowException()
        {
            //arrange
            _mockedRepository.Setup(r => r.Update(It.IsAny<PersonModel>())).Returns(
                (PersonModel target) =>
                {
                    throw new Exception();
                });
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.UpdateOne(_person, "2"); }
            //assert
            catch (FaultException e)
            {
                Assert.AreEqual("FAILED", e.Message);

                throw e;
            }
        }
    }
}
