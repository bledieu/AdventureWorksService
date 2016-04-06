using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventureWorks.Core.Person;
using System.Collections.Generic;
using AdventureWorks.Model;
using Moq;
using System.ServiceModel;
using AdventureWorks.Core;

namespace AdventureWorks.UnitTests.Person
{
    [TestClass]
    public class ServicePerson_GetAll : AServicePersonTest
    {
        [TestMethod]
        public void GetAllReturnsAllPerson()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);

            //act
            var result = controller.GetAll();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(50, result.Count);
            Assert.IsInstanceOfType(result, typeof(IList<PersonModel>));
            _mockedRepository.Verify(x => x.SelectAll(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ExceptionDuringGetAllThrowAnException()
        {
            _mockedRepository.Setup(r => r.SelectAll()).Returns(
                (PersonModel target) =>
                {
                    throw new Exception();
                }
                );

            ServicePerson controller = new ServicePerson(_repository);
            try { controller.GetAll(); }
            catch (Exception e)
            {
                Assert.AreEqual("FAILED", e.Message);
                throw e;
            }
        }

    }
}
