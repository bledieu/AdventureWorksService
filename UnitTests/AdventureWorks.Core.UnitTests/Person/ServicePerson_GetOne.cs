using AdventureWorks.Core;
using AdventureWorks.Core.Person;
using AdventureWorks.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.UnitTests.Person
{
    [TestClass]
    public class ServicePerson_GetOne : AServicePersonTest
    {
        [TestMethod]
        public void GetByIdReturnsCorrectPerson()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);
            PersonModel expectedPerson = _repository.SelectAll().Where(x => x.Id == 10).SingleOrDefault();
            Assert.IsNotNull(expectedPerson);

            //act
            PersonModel result = controller.GetOne("10");

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedPerson, result);
            _mockedRepository.Verify(x => x.GetOneById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void GetWithANullIdThrowException()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.GetOne(""); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("GetOne", e.Detail.Method);
                Assert.AreEqual("NULL_ARGUMENT", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void GetWithABadIdThrowException()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.GetOne("250"); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("GetOne", e.Detail.Method);
                Assert.AreEqual("UNKNOWN", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void NullReturnsFromDalThrowException()
        {
            //arrange
            _mockedRepository.Setup(r => r.GetOneById(It.IsAny<int>())).Returns((int id) => { return null; });
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.GetOne("2"); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("GetOne", e.Detail.Method);
                Assert.AreEqual("UNKNOWN", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ExceptionFromDalThrowException()
        {
            //arrange
            _mockedRepository.Setup(r => r.GetOneById(It.IsAny<int>())).Returns((int id) => { throw new Exception(); });
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.GetOne("2"); }
            //assert
            catch (FaultException e)
            {
                Assert.AreEqual("FAILED", e.Message);

                throw e;
            }
        }
    }
}
