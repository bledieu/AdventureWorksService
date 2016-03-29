using System;
using AdventureWorks.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AdventureWorks.Model.Tests.Person
{
    [TestClass]
    public class PersonModelTest
    {
        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void ConstructorShouldSetProperties()
        {
            //arrange
            //act
            PersonModel person = new PersonModel(1, PersonType.EM, "Mr.", "Baptiste", "LEDIEU");
            //assert
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual(PersonType.EM, person.Type);
            Assert.AreEqual("Mr.", person.Title);
            Assert.AreEqual("Baptiste", person.FirstName);
            Assert.AreEqual("LEDIEU", person.LastName);
        }

        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void TypeShouldBeEmIfNotSet()
        {
            PersonModel person = new PersonModel(1, "Mr.", "Baptiste", "LEDIEU");
            Assert.AreEqual(PersonType.EM, person.Type);
        }

        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void TitleShouldBeNullIfSetEmpty()
        {
            PersonModel person = new PersonModel(1, PersonType.EM, "", "Baptiste", "LEDIEU");
            Assert.AreEqual(null, person.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestCategory("PersonModelTest")]
        public void ConstructorShouldThrowExceptionIfIdNegative()
        {
            new PersonModel(-1, PersonType.EM, "Mr.", "Baptiste", "LEDIEU");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("PersonModelTest")]
        public void ConstructorShouldThrowExceptionIfFirstNameEmpty()
        {
            new PersonModel(1, PersonType.EM, "Mr.", "", "LEDIEU");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("PersonModelTest")]
        public void ConstructorShouldThrowExceptionIfLastNameEmpty()
        {
            new PersonModel(1, PersonType.EM, "Mr.", "Baptiste", "");
        }

        [TestMethod]
        [TestCategory("PersonModelTest")]
        public void PersonWithSameIdShouldBeEqual()
        {
            //arrange
            PersonModel person1 = new PersonModel(1, PersonType.EM, "", "Baptiste", "LEDIEU");
            PersonModel person2 = new PersonModel(1, PersonType.EM, "", "Baptiste", "LEDIEU");
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
            PersonModel person1 = new PersonModel(1, PersonType.EM, "", "Baptiste", "LEDIEU");
            PersonModel person2 = new PersonModel(2, PersonType.EM, "", "Baptiste", "LEDIEU");
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
