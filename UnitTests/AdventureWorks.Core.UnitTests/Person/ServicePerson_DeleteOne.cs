using AdventureWorks.Core;
using AdventureWorks.Core.Person;
using AdventureWorks.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.ServiceModel;

namespace AdventureWorks.UnitTests.Person
{
    [TestClass]
    public class ServicePerson_DeleteOne : AServicePersonTest
    {
        [TestMethod]
        public void DeletePersonByIdRemoveFromTheList()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);
            PersonModel personToDelete = _repository.SelectAll().Where(x => x.Id == 10).SingleOrDefault();
            int countBefore = _repository.SelectAll().Count;
            
            //act
            controller.DeleteOne("10");
            PersonModel personDeleted = _repository.SelectAll().Where(x => x.Id == 10).SingleOrDefault();
            int countAfter = _repository.SelectAll().Count;

            //assert
            Assert.IsNull(personDeleted);
            _mockedRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(countBefore - 1, countAfter);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void DeleteByBadIdReturnsException()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.DeleteOne("947856"); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("DeleteOne", e.Detail.Method);
                Assert.AreEqual("UNKNOWN", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<CoreDetailedException>))]
        public void DeleteWithANullIdThrowException()
        {
            //arange
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.DeleteOne(""); }
            //assert
            catch (FaultException<CoreDetailedException> e)
            {
                Assert.AreEqual("ServicePerson", e.Detail.Class);
                Assert.AreEqual("DeleteOne", e.Detail.Method);
                Assert.AreEqual("NULL_ARGUMENT", e.Detail.Family);

                throw e;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ExceptionDuringDalThrowException()
        {
            //arrange
            _mockedRepository.Setup(r => r.Delete(It.IsAny<int>())).Returns(
                (PersonModel target) =>
                {
                    throw new Exception();
                });
            ServicePerson controller = new ServicePerson(_repository);

            //act
            try { controller.DeleteOne("2"); }
            //assert
            catch (FaultException e)
            {
                Assert.AreEqual("FAILED", e.Message);

                throw e;
            }
        }
    }
}
