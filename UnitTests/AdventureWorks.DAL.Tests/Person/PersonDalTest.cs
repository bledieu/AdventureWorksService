using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventureWorks.Dal;
using System.Collections.Generic;
using AdventureWorks.Model;
using Moq;
using System.Linq;

namespace AdventureWorks.Dal.Tests
{
    [TestClass]
    public class PersonDalTest
    {
        private readonly string _connectionStringTestDb = @"Data Source=SVR-STONER.tesfri.intra\HERMES;Initial Catalog=AdventureWorks2012;Persist Security Info=False;User ID=team_project;Password=K@r@m@z0v;Connect Timeout=5;";

        public static Mock<IPersonDal> GetMockedRepository()
        {
            #region List of mocked person

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
            #endregion

            #region List of mocked login

            IList<LoginModel> logins = new List<LoginModel>
            {
                new LoginModel() { Email = "terri0@adventure-works.com", PasswordSalt = "EjJaC3U=", PasswordHash = "OZJpfqx/0EfhXWMuZ4XIk7IuSAuzxzZPyacbTbUUJefXmvOwv453rQin7pfUpzYW/k1LigbAoOJtGaTF21Kghw==", },
                new LoginModel() { Email = "roberto0@adventure-works.com", PasswordSalt = "wbPZqMw=", PasswordHash = "8BUXrZfDqO1IyHCWOYzYmqN1IhTUn3CJMpdx/UCQ3iY=", },
                new LoginModel() { Email = "rob0@adventure-works.com", PasswordSalt = "PwSunQU=", PasswordHash = "SjLXpiarHSlz+6AG+H+4QpB/IPRzras/+9q/5Wr7tf8=", },
                new LoginModel() { Email = "gail0@adventure-works.com", PasswordSalt = "qYhZRiM=", PasswordHash = "8FYdAiY6gWuBsgjCFdg0UibtsqOcWHf9TyaHIP7+paA=", },
                new LoginModel() { Email = "jossef0@adventure-works.com", PasswordSalt = "a9GiLUA=", PasswordHash = "u5kbN5n84NRE1h/a+ktdRrXucjgrmfF6wZC4g82rjHM=", },
                new LoginModel() { Email = "dylan0@adventure-works.com", PasswordSalt = "13mu8BA=", PasswordHash = "zSqerln8T8eq3nYHC4Lx4vMuxZaxkDylVwWnP2ZT6QA=", },
                new LoginModel() { Email = "diane1@adventure-works.com", PasswordSalt = "FlCpzTU=", PasswordHash = "s+FUWADIZzXBKpcbxe4OwL2uiJmjLogJNYXXHvc1X/k=", },
                new LoginModel() { Email = "gigi0@adventure-works.com", PasswordSalt = "FTcZMvQ=", PasswordHash = "fCvCTy3RwzA2LNhhhYUbT7erkb9Au5wyM2q7ReHroV0=", },
                new LoginModel() { Email = "michael6@adventure-works.com", PasswordSalt = "K7dMpTY=", PasswordHash = "/8biMrxuAtETGeIuloSrMQHBraZtZ+eU2z5OJ1Fhn6M=", },
                new LoginModel() { Email = "ovidiu0@adventure-works.com", PasswordSalt = "wTGciQ8=", PasswordHash = "iaZ6ky76dbOG+0Y069v4bm78UfhfGXSeYtxp4Vgd15o=", },
                new LoginModel() { Email = "thierry0@adventure-works.com", PasswordSalt = "S0g9tDo=", PasswordHash = "I9HGCr3jbwF3LYBlVsM/cOC2IHfg7ns5t2xejnWZ9Ko=", },
                new LoginModel() { Email = "janice0@adventure-works.com", PasswordSalt = "S7XWeXc=", PasswordHash = "3ZuoojogvvBKmtr+iHeqWoiZNCzp6N1abPoymjp+O+4=", },
                new LoginModel() { Email = "michael8@adventure-works.com", PasswordSalt = "BpZw68c=", PasswordHash = "mKAlVVzgDo12qro7ZDmr3oEmCxklWc7+dYDFlC58Vv4=", },
                new LoginModel() { Email = "sharon0@adventure-works.com", PasswordSalt = "dhGWm88=", PasswordHash = "m5ESf7VNdqsqa/s82jX/r3UK95GTrMPM4YhGw0jzS9c=", },
                new LoginModel() { Email = "david0@adventure-works.com", PasswordSalt = "CTdtN+Q=", PasswordHash = "oaeJoTn5hbyNfemp2qzIpGTP5uNle8NRPki9Ur3Znl8=", },
                new LoginModel() { Email = "kevin0@adventure-works.com", PasswordSalt = "nqdPuIs=", PasswordHash = "7OdV1zJ/Q0TtaAgSo1KHZAFaUhN0XjqzL9gXrnpl5BE=", },
                new LoginModel() { Email = "john5@adventure-works.com", PasswordSalt = "5lT5pzE=", PasswordHash = "+Six1+I1JOOR+oosTOz1L7jf/t79CUdo05d5uv+scXE=", },
                new LoginModel() { Email = "mary2@adventure-works.com", PasswordSalt = "9S8bKmU=", PasswordHash = "+NeFaMBZDuBHjM/jVl5RQmWChbKw5O6H7LR8DAboLXc=", },
                new LoginModel() { Email = "wanida0@adventure-works.com", PasswordSalt = "QxeRrms=", PasswordHash = "QK5+W4y+v4xBQ7/grkmXPa5kK5G8kMb5qJZLpDzpLko=", },
                new LoginModel() { Email = "terry0@adventure-works.com", PasswordSalt = "UDnl8iQ=", PasswordHash = "b8i62trjF69At8pO1r2uClfiGWq0wx7w8Kz8xsOVmSs=", },
                new LoginModel() { Email = "sariya0@adventure-works.com", PasswordSalt = "oK/J3Z0=", PasswordHash = "NrVTt2LIAOOd/kB3YfCaKzLbeOgefZsGTeg2z7TKzhw=", },
                new LoginModel() { Email = "mary0@adventure-works.com", PasswordSalt = "xW40Ctk=", PasswordHash = "TlZs9R2WoVp1jjfCyxE8Dtaw4OhlcKgCPbqXaOxeIW4=", },
                new LoginModel() { Email = "jill0@adventure-works.com", PasswordSalt = "Cog9m/Y=", PasswordHash = "wI5v4SNv5Mg5ea0Ufy1xy966PrXWCBp8gLIhC1QTqWw=", },
                new LoginModel() { Email = "james1@adventure-works.com", PasswordSalt = "cHNglYA=", PasswordHash = "iYoywqstKDflRLmJwKmI/ObzZl6KQ4sX5hUgBa1Qgz0=", },
                new LoginModel() { Email = "peter0@adventure-works.com", PasswordSalt = "FKGTmOE=", PasswordHash = "GCZ//7zDxPTsO9cuY1Rk0uUUc6t2jEGZwjGqXVll6Ws=", },
                new LoginModel() { Email = "jo0@adventure-works.com", PasswordSalt = "+QS/bjA=", PasswordHash = "uQaHsaafTmDMFjX9nNhAFjHaYjqeTZhlcZp4azb5Hgs=", },
                new LoginModel() { Email = "guy1@adventure-works.com", PasswordSalt = "Lanmhoo=", PasswordHash = "Li2+pm3yhRgX8v0v3JJZWL8NPgdBse1zAG1ph25sFAk=", },
                new LoginModel() { Email = "mark1@adventure-works.com", PasswordSalt = "a1y2pAU=", PasswordHash = "2sudtPUZMsjMIVXKcfx3NPWhTws5ZSHJa+pzKYnyxtU=", },
                new LoginModel() { Email = "britta0@adventure-works.com", PasswordSalt = "iZ1gk4w=", PasswordHash = "xyWYdzg9o+gUyBlMkxVRx4O4oBuaXtien0ZXxhB58gM=", },
                new LoginModel() { Email = "margie0@adventure-works.com", PasswordSalt = "sIEEdsc=", PasswordHash = "kJbBLUOic5PVfwg1GdR9qfyoMjvORTYcGZodSeVZWIk=", },
                new LoginModel() { Email = "rebecca0@adventure-works.com", PasswordSalt = "wWhazQ0=", PasswordHash = "eNEOaY4IjJnWBTMEi5RwtrUvNsnE2JdKomG3EtXh0DI=", },
                new LoginModel() { Email = "annik0@adventure-works.com", PasswordSalt = "O6qnszI=", PasswordHash = "imO5JUOoa0oxbLl8+7IFkDSXBvSPkWMIQ+LyZweF5JU=", },
                new LoginModel() { Email = "suchitra0@adventure-works.com", PasswordSalt = "fV77lec=", PasswordHash = "8gRugQzLi5VXNW0Nf8zZVbfXgEugWH0RjY+Q+HMyuVY=", },
                new LoginModel() { Email = "brandon0@adventure-works.com", PasswordSalt = "/e8ZHbc=", PasswordHash = "aG4eXJgOVz70IbhQ0E+86zZ73KYXXw8dPt3YCyGZeE8=", },
                new LoginModel() { Email = "jose0@adventure-works.com", PasswordSalt = "LzNcnKc=", PasswordHash = "JayoOBYFOeSNXJFNFOCCpaxrgbjZo0KR1GhtVts+TGs=", },
                new LoginModel() { Email = "chris2@adventure-works.com", PasswordSalt = "T/vRqY0=", PasswordHash = "lZfD5IydVoTG3D4Kl+Rlu/4zC1Ws/KvjgNz7S146TDo=", },
                new LoginModel() { Email = "kim1@adventure-works.com", PasswordSalt = "wdPlbgo=", PasswordHash = "vjjIYGB99tLATbne8D7MRTg2+tkExYHjmH/pcWSg3nE=", },
                new LoginModel() { Email = "ed0@adventure-works.com", PasswordSalt = "QxftJhk=", PasswordHash = "1qBCXoN8hcC6AXCLJ+mHECaq2uadfZjUhsZ0T9VOrCQ=", },
                new LoginModel() { Email = "jolynn0@adventure-works.com", PasswordSalt = "zS9+W2w=", PasswordHash = "8bitCdCd+ipeWv1IH6koUF/Kj+5LnT4/9Upfr2aW/JY=", },
                new LoginModel() { Email = "bryan0@adventure-works.com", PasswordSalt = "f64DkCs=", PasswordHash = "0DC43UiCe4QCmUgrnFd0Lur1C0rfS+Ht1Z8sgyk7wlM=", },
                new LoginModel() { Email = "james0@adventure-works.com", PasswordSalt = "uTuRBuI=", PasswordHash = "HSLAA7MxklY4dZIcbcNYGiUvPkEi4tyG/U0WE76uBag=", },
                new LoginModel() { Email = "nancy0@adventure-works.com", PasswordSalt = "6f2hjx4=", PasswordHash = "n2QKI23Ms1v3+43ei7hm1pUh6SkQcc5J3r52yMYeNTA=", },
                new LoginModel() { Email = "simon0@adventure-works.com", PasswordSalt = "Tc2WN/g=", PasswordHash = "IoIaCj49Zj5/06VGOxs/PBaHj8A5PNmtkP6GD1VdKA8=", },
                new LoginModel() { Email = "thomas0@adventure-works.com", PasswordSalt = "x8a2Ne8=", PasswordHash = "bNLcjIkHF9PhhLoHDcoQMFdZqro/2LhO6rbZEXwuXgA=", },
                new LoginModel() { Email = "eugene1@adventure-works.com", PasswordSalt = "5Tst9Lo=", PasswordHash = "FRAoiZRV3HyWmSEykYZOHNH2YwpT2Vkp+8vfSVY+4Yo=", },
                new LoginModel() { Email = "andrew0@adventure-works.com", PasswordSalt = "ZymdKp8=", PasswordHash = "OZg1gWJ8cJwFeGr4+7FZ+MqKBARAS7J+8B1pFKa1XcQ=", },
                new LoginModel() { Email = "ruth0@adventure-works.com", PasswordSalt = "9jpCCt8=", PasswordHash = "1RPeKdRfcI8Mq7HMufSsh7rQytv9C6RjCCFZQdVDyDU=", },
                new LoginModel() { Email = "barry0@adventure-works.com", PasswordSalt = "/YY4OC8=", PasswordHash = "hMm7dGpKsLImpDz599yNKq2r505OJ6gWa/S367qozpc=", },
                new LoginModel() { Email = "sidney0@adventure-works.com", PasswordSalt = "CfjZvwk=", PasswordHash = "ZozTjNcGBqxrOfHFyynKlPjbW7aAWJ1iEYr2YlRK2tc=", },
                new LoginModel() { Email = "jeffrey0@adventure-works.com", PasswordSalt = "YH+8tA4=", PasswordHash = "W9SR91EhQQG8SpnY54G6CvXc8yTvDcQWzO2hFbc9idI=", },
            };
            #endregion

            Mock<IPersonDal> repository = new Mock<IPersonDal>();
            repository.Setup(r => r.SelectAll()).Returns(persons);
            repository.Setup(r => r.GetOneById(It.IsAny<int>())).Returns((int id) => persons.Where(x => x.Id == id).SingleOrDefault());
            repository.Setup(r => r.Insert(It.IsAny<PersonModel>())).Returns(
                (PersonModel target) =>
                    {
                        if (target.Id != default(int)) return null;

                        target.Id = persons.Max(x => x.Id) + 1;
                        target.ModifiedDate = DateTime.Now;
                        persons.Add(target);

                        return target;
                    }
                );
            repository.Setup(r => r.Update(It.IsAny<PersonModel>())).Returns(
                (PersonModel target) =>
                    {
                        var original = persons.Where(q => q.Id == target.Id).SingleOrDefault();
                        if (original == null) return false;

                        original.Title = target.Title;
                        original.Type = target.Type;
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
            repository.Setup(r => r.SelectPassword(It.IsAny<string>())).Returns((string email) => logins.Where(l => l.Email == email).SingleOrDefault());


            return repository;
        }

        private readonly IPersonDal _repository;

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
            newPerson = this._repository.Insert(newPerson);
            Assert.IsNotNull(newPerson);

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
            newPerson = this._repository.Insert(newPerson);
            Assert.IsNull(newPerson);
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

        [TestMethod]
        [TestCategory("PersonDal")]
        public void SelectPasswordReturnCorrectLogin()
        {
            string email = "terri0@adventure-works.com";
            LoginModel login = _repository.SelectPassword(email);

            Assert.IsNotNull(login);
            Assert.AreEqual(email, login.Email);
            Assert.AreEqual("EjJaC3U=", login.PasswordSalt);
            Assert.AreEqual("OZJpfqx/0EfhXWMuZ4XIk7IuSAuzxzZPyacbTbUUJefXmvOwv453rQin7pfUpzYW/k1LigbAoOJtGaTF21Kghw==", login.PasswordHash);
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

        [TestMethod]
        [TestCategory("PersonDal RealDb")]
        public void SelectPasswordReturnCorrectLoginFromDb()
        {
            string email = "terri0@adventure-works.com";

            PersonDal dal = new PersonDal(_connectionStringTestDb);
            LoginModel login = dal.SelectPassword(email);

            Assert.IsNotNull(login);
            Assert.AreEqual(email, login.Email);
            Assert.AreEqual("EjJaC3U=", login.PasswordSalt);
            Assert.AreEqual("OZJpfqx/0EfhXWMuZ4XIk7IuSAuzxzZPyacbTbUUJefXmvOwv453rQin7pfUpzYW/k1LigbAoOJtGaTF21Kghw==", login.PasswordHash);
        }

        #endregion

    }
}
