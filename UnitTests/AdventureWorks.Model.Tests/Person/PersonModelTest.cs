using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdventureWorks.Model.Tests.Person
{
    [TestClass]
    public class PersonModelTest
    {
        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void TypeShouldBeEmIfNotSet()
        {
            PersonModel person = new PersonModel();
            Assert.AreEqual(PersonType.EM, person.Type);
        }

        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void TitleShouldBeNullIfSetEmpty()
        {
            PersonModel person = new PersonModel() { Title = "", };
            Assert.AreEqual(null, person.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestCategory("PersonModelTest")]
        public void IDThrowExceptionIfIdNegative()
        {
            PersonModel person = new PersonModel() { Id = -1, };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("PersonModelTest")]
        public void FirstNameShouldThrowExceptionIfEmpty()
        {
            new PersonModel() { FirstName = "", };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("PersonModelTest")]
        public void LastNameShouldThrowExceptionIfEmpty()
        {
            new PersonModel() { LastName = "", };
        }

        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void PersonWithSameIdShouldBeEqual()
        {
            //arrange
            PersonModel person1 = new PersonModel() { Id = 1, Type = PersonType.EM, FirstName = "Baptiste", LastName = "LEDIEU", };
            PersonModel person2 = new PersonModel() { Id = 1, Type = PersonType.EM, FirstName = "Baptiste", LastName = "LEDIEU", };
            //act
            //assert
            Assert.AreEqual(person1, person2);
            Assert.AreEqual(person1.GetHashCode(), person2.GetHashCode());
        }

        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void PersonWithDifferentIdShouldNotBeEqual()
        {
            //arrange
            PersonModel person1 = new PersonModel() { Id = 1, Type = PersonType.EM, FirstName = "Baptiste", LastName = "LEDIEU", };
            PersonModel person2 = new PersonModel() { Id = 2, Type = PersonType.EM, FirstName = "Baptiste", LastName = "LEDIEU", };
            //act
            //assert
            Assert.AreNotEqual(person1, person2);
            Assert.AreNotEqual(person1.GetHashCode(), person2.GetHashCode());
        }

        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void ModifiedDateStringAndModifiedDateShouldBeSynchronized()
        {
            DateTime now = DateTime.Now;

            PersonModel person = new PersonModel { ModifiedDate = now };

            Assert.AreNotEqual(default(DateTime), person.ModifiedDate);
            Assert.AreNotEqual(default(string), person.ModifiedDateString);
            Assert.AreEqual(now.ToString("O"), person.ModifiedDateString);

            string nowStr = DateTime.Now.ToString("O");

            person.ModifiedDateString = nowStr;
            Assert.AreEqual(nowStr, person.ModifiedDateString);
            Assert.AreEqual(Convert.ToDateTime(nowStr), person.ModifiedDate);
        }

        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void TypeStringAndTypeShouldBeSynchronized()
        {
            PersonType type = PersonType.GC;

            PersonModel person = new PersonModel { Type = type};

            Assert.AreNotEqual(default(PersonType), person.Type);
            Assert.AreNotEqual(default(string), person.TypeString);
            Assert.AreEqual("GC", person.TypeString);

            person.TypeString = "VC";
            Assert.AreEqual(PersonType.VC, person.Type);
        }

        [TestMethod]
        [TestCategory("PersonModelTest")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyModifiedDateStringShouldThrowArgumentNullException()
        {
            PersonModel person = new PersonModel { ModifiedDateString = null };
        }

       
    }
}
