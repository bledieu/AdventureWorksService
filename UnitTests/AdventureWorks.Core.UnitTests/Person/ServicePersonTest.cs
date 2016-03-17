using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AventureWorks.Core.Person;
using AdventureWorks.Model.Person;
using System.Collections.Generic;
using AdventureWorks.Dal;
using AdventureWorks.Dal.Tests;
using System.Linq;

namespace AdventureWorks.UnitTests.Person
{
    [TestClass]
    public class ServicePersonTest
    {
        private readonly Mock<IRepositoryT<PersonModel>> _mockedRepository;
        private readonly IRepositoryT<PersonModel> _repository;

        public ServicePersonTest()
        {
            _mockedRepository = PersonDalTest.GetMockedRepository();
            _repository = _mockedRepository.Object;
        }

        [TestMethod]
        [TestCategory("ServicePersonTest")]
        public void GetAllReturnsAllPerson()
        {
            //arrange
            ServicePerson controller = new ServicePerson(_repository);

            //act
            IList<PersonModel> result = controller.GetAll();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(50, result.Count);
            _mockedRepository.Verify(x => x.SelectAll(), Times.Once);
        }

        [TestMethod]
        [TestCategory("ServicePersonTest")]
        public void SelectByIdReturnsCorrectPerson()
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
        [TestCategory("ServicePersonTest")]
        public void WhenInsertThenPersitsInDb()
        {
            //arrange
            PersonModel newPerson = new PersonModel(0, PersonType.GC, "Mr.", "John", "Smith");
            ServicePerson controller = new ServicePerson(_repository);

            int personsCount = _repository.SelectAll().Count();

            //act
            var result = controller.AddOne(newPerson);

            //assert
            Assert.AreEqual(personsCount + 1, _repository.SelectAll().Count());
            _mockedRepository.Verify(x => x.Insert(It.IsAny<PersonModel>()), Times.Once);
        }
    }
}
