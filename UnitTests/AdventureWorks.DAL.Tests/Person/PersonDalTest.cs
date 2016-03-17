using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventureWorks.Dal;
using System.Collections.Generic;
using AdventureWorks.Model.Person;
using Moq;
using System.Linq;

namespace AdventureWorks.Dal.Tests
{
    [TestClass]
    public class PersonDalTest
    {
        private readonly string _connectionStringTestDb = @"Data Source=SVR-STONER.tesfri.intra\HERMES;Initial Catalog=AdventureWorks2012;Persist Security Info=False;User ID=team_project;Password=K@r@m@z0v;Connect Timeout=5;";

        public static Mock<IRepositoryT<PersonModel>> GetMockedRepository()
        {
            IList<PersonModel> persons = new List<PersonModel>
            {
                new PersonModel(2, PersonType.EM, "", "Terri", "Duffy"),
                new PersonModel(3, PersonType.EM, "", "Roberto", "Tamburello"),
                new PersonModel(4, PersonType.EM, "", "Rob", "Walters"),
                new PersonModel(5, PersonType.EM, "Ms.", "Gail", "Erickson"),
                new PersonModel(6, PersonType.EM, "Mr.", "Jossef", "Goldberg"),
                new PersonModel(7, PersonType.EM, "", "Dylan", "Miller"),
                new PersonModel(8, PersonType.EM, "", "Diane", "Margheim"),
                new PersonModel(9, PersonType.EM, "", "Gigi", "Matthew"),
                new PersonModel(10, PersonType.EM, "", "Michael", "Raheem"),
                new PersonModel(11, PersonType.EM, "", "Ovidiu", "Cracium"),
                new PersonModel(12, PersonType.EM, "", "Thierry", "D'Hers"),
                new PersonModel(13, PersonType.EM, "Ms.", "Janice", "Galvin"),
                new PersonModel(14, PersonType.EM, "", "Michael", "Sullivan"),
                new PersonModel(15, PersonType.EM, "", "Sharon", "Salavaria"),
                new PersonModel(16, PersonType.EM, "", "David", "Bradley"),
                new PersonModel(17, PersonType.EM, "", "Kevin", "Brown"),
                new PersonModel(18, PersonType.EM, "", "John", "Wood"),
                new PersonModel(19, PersonType.EM, "", "Mary", "Dempsey"),
                new PersonModel(20, PersonType.EM, "", "Wanida", "Benshoof"),
                new PersonModel(21, PersonType.EM, "", "Terry", "Eminhizer"),
                new PersonModel(22, PersonType.EM, "", "Sariya", "Harnpadoungsataya"),
                new PersonModel(23, PersonType.EM, "", "Mary", "Gibson"),
                new PersonModel(24, PersonType.EM, "Ms.", "Jill", "Williams"),
                new PersonModel(25, PersonType.EM, "", "James", "Hamilton"),
                new PersonModel(26, PersonType.EM, "", "Peter", "Krebs"),
                new PersonModel(27, PersonType.EM, "", "Jo", "Brown"),
                new PersonModel(28, PersonType.EM, "", "Guy", "Gilbert"),
                new PersonModel(29, PersonType.EM, "", "Mark", "McArthur"),
                new PersonModel(30, PersonType.EM, "", "Britta", "Simon"),
                new PersonModel(31, PersonType.EM, "", "Margie", "Shoop"),
                new PersonModel(32, PersonType.EM, "", "Rebecca", "Laszlo"),
                new PersonModel(33, PersonType.EM, "", "Annik", "Stahl"),
                new PersonModel(34, PersonType.EM, "", "Suchitra", "Mohan"),
                new PersonModel(35, PersonType.EM, "", "Brandon", "Heidepriem"),
                new PersonModel(36, PersonType.EM, "", "Jose", "Lugo"),
                new PersonModel(37, PersonType.EM, "", "Chris", "Okelberry"),
                new PersonModel(38, PersonType.EM, "", "Kim", "Abercrombie"),
                new PersonModel(39, PersonType.EM, "", "Ed", "Dudenhoefer"),
                new PersonModel(40, PersonType.EM, "", "JoLynn", "Dobney"),
                new PersonModel(41, PersonType.EM, "", "Bryan", "Baker"),
                new PersonModel(42, PersonType.EM, "", "James", "Kramer"),
                new PersonModel(43, PersonType.EM, "", "Nancy", "Anderson"),
                new PersonModel(44, PersonType.EM, "", "Simon", "Rapier"),
                new PersonModel(45, PersonType.EM, "", "Thomas", "Michaels"),
                new PersonModel(46, PersonType.EM, "", "Eugene", "Kogan"),
                new PersonModel(47, PersonType.EM, "", "Andrew", "Hill"),
                new PersonModel(48, PersonType.EM, "", "Ruth", "Ellerbrock"),
                new PersonModel(49, PersonType.EM, "", "Barry", "Johnson"),
                new PersonModel(50, PersonType.EM, "", "Sidney", "Higa"),
                new PersonModel(51, PersonType.EM, "", "Jeffrey", "Ford"),
            };

            Mock<IRepositoryT<PersonModel>> repository = new Mock<IRepositoryT<PersonModel>>();
            repository.Setup(r => r.SelectAll()).Returns(persons);
            repository.Setup(r => r.GetOneById(It.IsAny<int>())).Returns((int id) => persons.Where(x => x.Id == id).SingleOrDefault());
            repository.Setup(r => r.Insert(It.IsAny<PersonModel>())).Returns(
                (PersonModel target) =>
                {
                    if (target.Id != default(int)) return false;

                    target.Id = persons.Max(x => x.Id) + 1;
                    target.ModifiedDate = DateTime.Now;
                    persons.Add(target);

                    return true;
                }
                );
            repository.Setup(r => r.Update(It.IsAny<PersonModel>())).Returns(
                (PersonModel target) =>
                {
                    var original = persons.Where(q => q.Id == target.Id).SingleOrDefault();
                    if (original == null) return false;

                    original.Title = target.Title;
                    original.FirstName = target.FirstName;
                    original.LastName = target.LastName;
                    original.ModifiedDate = DateTime.Now;

                    return true;
                }
                );
            repository.Setup(r => r.Delete(It.IsAny<int>())).Returns(
                (int target) =>
                {
                    var person = persons.Where(q => q.Id == target).SingleOrDefault();
                    if (person == null) return false;

                    return persons.Remove(person);
                }
                );

            return repository;
        }

        private readonly IRepositoryT<PersonModel> _repository;

        public PersonDalTest()
        {
            _repository = PersonDalTest.GetMockedRepository().Object;
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void ConstructorShouldSetPrivateConnectionString()
        {
            //arrange
            string connectionString = @"Data Source=SVR-STONER.tesfri.intra\HERMES;Initial Catalog=AdventureWorks2012;Persist Security Info=False;User ID=team_project;Password=K@r@m@z0v;Connect Timeout=5;";
            //act
            PersonDal person = new PersonDal(connectionString);
            //assert
            Assert.AreEqual(connectionString, person.ConnectionString);
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void CanReturnAllPersons()
        {
            IList<PersonModel> persons = _repository.SelectAll();

            Assert.IsNotNull(persons);
            Assert.AreNotEqual(0, persons.Count);
            CollectionAssert.AllItemsAreUnique((System.Collections.ICollection)persons);
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void CanReturnPersonById()
        {
            PersonModel person = _repository.GetOneById(10);

            Assert.IsNotNull(person);
            Assert.IsInstanceOfType(person, typeof(PersonModel));
            Assert.AreEqual(10, person.Id);
            Assert.AreEqual("Raheem", person.LastName);
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void CanInsertPerson()
        {
            // Create a new person, not I do not supply an id
            PersonModel newPerson = new PersonModel(0, "Mr.", "John", "Doe");

            int personCount = _repository.SelectAll().Count;

            Assert.AreEqual(50, personCount); // Verify the expected Number pre-insert

            // Try saving our new person
            bool result = this._repository.Insert(newPerson);
            Assert.IsTrue(result);

            // demand a recount
            personCount = _repository.SelectAll().Count;
            Assert.AreEqual(51, personCount); // Verify the expected Number post-insert

            // verify that our new product has been saved
            PersonModel testPerson = _repository.SelectAll().OrderByDescending(x => x.Id).FirstOrDefault();

            Assert.IsNotNull(testPerson); // Test if null

            Assert.IsInstanceOfType(testPerson, typeof(PersonModel)); // Test type
            Assert.AreEqual(52, testPerson.Id); // Verify it has the expected personid
            Assert.AreEqual(newPerson.LastName, testPerson.LastName);
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void CantInsertAnExistingPerson()
        {
            // Create a new person, not I do not supply an id
            PersonModel newPerson = _repository.GetOneById(15);
            Assert.IsNotNull(newPerson);

            // Try saving our new person
            bool result = this._repository.Insert(newPerson);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void CanUpdatePerson()
        {
            string newLastName = "new lastName";
            // Find a product by id
            PersonModel testPerson = _repository.GetOneById(10);

            Assert.AreNotEqual(newLastName, testPerson.LastName);

            // Change one of its properties
            testPerson.LastName = newLastName;
            DateTime modifiedDate = testPerson.ModifiedDate;

            // Save our changes.
            bool result = _repository.Update(testPerson);
            Assert.IsTrue(result);

            // Verify the change
            testPerson = _repository.GetOneById(10);

            Assert.AreEqual(newLastName, testPerson.LastName);
            Assert.IsTrue(testPerson.ModifiedDate > modifiedDate);
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void CantUpdateAnUnexistingPerson()
        {
            string newLastName = "new lastName";
            // Find a product by id
            PersonModel testPerson = _repository.GetOneById(10);

            Assert.AreNotEqual(newLastName, testPerson.LastName);

            // Change one of its properties
            testPerson = new PersonModel(int.MaxValue, testPerson.Type, testPerson.Title, testPerson.FirstName, testPerson.LastName);
            testPerson.LastName = newLastName;
            DateTime modifiedDate = testPerson.ModifiedDate;

            // Save our changes.
            bool result = _repository.Update(testPerson);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void CanDeletePerson()
        {
            // Verify the expected Number pre-delete
            int personCount = _repository.SelectAll().Count;
            Assert.AreEqual(50, personCount);

            PersonModel person = _repository.GetOneById(20);
            Assert.IsNotNull(person);

            // Try deleting our person
            bool result = this._repository.Delete(20);
            Assert.IsTrue(result);

            // demand a recount
            personCount = _repository.SelectAll().Count;
            Assert.AreEqual(49, personCount); // Verify the expected Number post-delete

            // verify that our product has been deleted
            person = _repository.GetOneById(20);

            Assert.IsNull(person); // Test if null
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void CantDeleteAnUnexistingPerson()
        {
            // Verify the expected Number pre-delete
            int personCount = _repository.SelectAll().Count;
            Assert.AreEqual(50, personCount);

            // Try deleting our person
            bool result = this._repository.Delete(int.MaxValue);
            Assert.IsFalse(result);
        }

        #region Test Real Db

        [TestMethod]
        [TestCategory("PersonDal RealDb")]
        public void CanReturnAllPersonsFromDb()
        {
            PersonDal dal = new PersonDal(_connectionStringTestDb);
            IList<PersonModel> persons = dal.SelectAll();

            Assert.IsNotNull(persons);
            Assert.AreNotEqual(0, persons.Count);
            CollectionAssert.AllItemsAreUnique((System.Collections.ICollection)persons);
        }

        #endregion

    }
}
