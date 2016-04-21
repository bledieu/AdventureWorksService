using AdventureWorks.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureWorks.Dal.Tests
{
    [TestClass]
    public class PersonDalTest
    {
        private readonly string _connectionStringTestDb = @"Data Source=SVR-STONER.tesfri.intra\HERMES;Initial Catalog=AdventureWorksDB;Persist Security Info=False;User ID=team_project;Password=K@r@m@z0v;Connect Timeout=5;";
        private readonly string _providerNameTestDb = @"System.Data.SqlClient";

        static int _originalPersonsCount = 0;
        static int _maxPersonID = 0;

        public static Mock<IPersonDal> GetMockedRepository()
        {
            #region List of mocked person

            IList<PersonModel> persons = new List<PersonModel>
            {
                new PersonWithCredentialModel() { Id = 1, Type = PersonType.VC, FirstName = "Gigi46-2", LastName = "dsfsdfsf3", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "gdsfsdfsf3@adventure-works.com", Salt = @"@JEassx5egT8KMOj]^yrI-2Y_", Hash = @"1B5AF6B79A25867C6734FB85DBAF31", },
                new PersonWithCredentialModel() { Id = 2, Type = PersonType.EM, FirstName = "Robert1-3", LastName = "Tamburello", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "rtamburello@adventure-works.com", Salt = @"@9MsQPOEF@#79VvCV8wFe-?@J", Hash = @"94AEB671E5D9FC57F05BC9C7C199CE", },
                new PersonWithCredentialModel() { Id = 3, Type = PersonType.EM, FirstName = "Rob-4", LastName = "Walters", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "rwalters@adventure-works.com", Salt = @"@9MsQPOEF@#79VvCV8wFe-?@J", Hash = @"39672DF2E73F81F27833A5201CB22C", },
                new PersonWithCredentialModel() { Id = 4, Type = PersonType.EM, Title = "Ms.", FirstName = "Gail-5", LastName = "Erickson", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "gerickson@adventure-works.com", Salt = @"@OBOSM6D ca>kei@7nnJAW@Us", Hash = @"5A2637FE317DD5800DACA005B8745A", },
                new PersonWithCredentialModel() { Id = 5, Type = PersonType.EM, Title = "Mr.", FirstName = "Jossef-6", LastName = "Goldberg", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "jgoldberg@adventure-works.com", Salt = @"@OBOSM6D ca>kei@7nnJAW@Us", Hash = @"1EAABD3DA8EC61577C11A9E13CAE1E", },
                new PersonWithCredentialModel() { Id = 6, Type = PersonType.EM, FirstName = "Dylan-7", LastName = "Miller", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "dmiller@adventure-works.com", Salt = @"@f6+UK|CX(AE?t[=vEdN|$Ak=", Hash = @"950503984A8099BA705A7EAA03EB77", },
                new PersonWithCredentialModel() { Id = 7, Type = PersonType.EM, FirstName = "Diane-8", LastName = "Margheim", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "dmargheim@adventure-works.com", Salt = @"@f6+UK|CX(AE?t[=vEdN|$Ak=", Hash = @"6EFF62613C6BB38D247BC8462B2D84", },
                new PersonWithCredentialModel() { Id = 8, Type = PersonType.EM, FirstName = "Gigi-9", LastName = "Matthew", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "gmatthew@adventure-works.com", Salt = @"@dfxwG\a&7uOc*i9l/wSl\Cht", Hash = @"357B66EAECB0CF97CE337C32AEF437", },
                new PersonWithCredentialModel() { Id = 9, Type = PersonType.EM, FirstName = "Michael-10", LastName = "Raheem", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "mraheem@adventure-works.com", Salt = @"@dfxwG\a&7uOc*i9l/wSl\Cht", Hash = @"634F0D9D33235C943527A5D0A64E43", },
                new PersonWithCredentialModel() { Id = 10, Type = PersonType.EM, FirstName = "Ovidiu-11", LastName = "Cracium", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "ocracium@adventure-works.com", Salt = @"@z[TyEC_^YUV79[6MdmWH(D ?", Hash = @"3E0FB664F00CA1C045158F824B4489", },
                new PersonWithCredentialModel() { Id = 11, Type = PersonType.EM, FirstName = "Thierry-12", LastName = "D'Hers", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "td'hers@adventure-works.com", Salt = @"@z[TyEC_^YUV79[6MdmWH(D ?", Hash = @"64557F4C133164312174865AB362F7", },
                new PersonWithCredentialModel() { Id = 12, Type = PersonType.EM, Title = "Ms.", FirstName = "Janice-13", LastName = "Galvin", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "jgalvin@adventure-works.com", Salt = @"@2P/{B+^9|5]iHN3.<d[%SE5g", Hash = @"94B6073C08F00CEE438F7FAE0BA773", },
                new PersonWithCredentialModel() { Id = 13, Type = PersonType.EM, FirstName = "Michael-14", LastName = "Sullivan", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "msullivan@adventure-works.com", Salt = @"@2P/{B+^9|5]iHN3.<d[%SE5g", Hash = @"6A7D62CF291A0127CE97589A307693", },
                new PersonWithCredentialModel() { Id = 14, Type = PersonType.EM, FirstName = "Sharon-15", LastName = "Salavaria", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "ssalavaria@adventure-works.com", Salt = @"@1!|@?i|e-hf/[[/$%v`s-F3@", Hash = @"0E075D76EADDD0A4E861FC1A80F7C9", },
                new PersonWithCredentialModel() { Id = 15, Type = PersonType.EM, FirstName = "David-16", LastName = "Bradley", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "dbradley@adventure-works.com", Salt = @"@1!|@?i|e-hf/[[/$%v`s-F3@", Hash = @"960FE275F0484DAAE5B2B7E62ED701", },
                new PersonWithCredentialModel() { Id = 16, Type = PersonType.EM, FirstName = "Kevin-17", LastName = "Brown", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "kbrown@adventure-works.com", Salt = @"@GtXB<Q{?PImajM,c[mdOWGHh", Hash = @"B0F00803DBCCBE3A05CD179380F34F", },
                new PersonWithCredentialModel() { Id = 17, Type = PersonType.EM, FirstName = "John-18", LastName = "Wood", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "jwood@adventure-works.com", Salt = @"@GtXB<Q{?PImajM,c[mdOWGHh", Hash = @"4D5997D7366C397FA14B34BAD00D14", },
                new PersonWithCredentialModel() { Id = 18, Type = PersonType.EM, FirstName = "Mary-19", LastName = "Dempsey", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "mdempsey@adventure-works.com", Salt = @"@]i4D98zwr)t6y@)C2cg,$H^3", Hash = @"E0F1CA65B97FBD170B72BEE89728E9", },
                new PersonWithCredentialModel() { Id = 19, Type = PersonType.EM, FirstName = "Wanida-20", LastName = "Benshoof", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "wbenshoof@adventure-works.com", Salt = @"@]i4D98zwr)t6y@)C2cg,$H^3", Hash = @"9C66AE82926284B0BED1CA43628324", },
                new PersonWithCredentialModel() { Id = 20, Type = PersonType.EM, FirstName = "Terry-21", LastName = "Eminhizer", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "teminhizer@adventure-works.com", Salt = @"@\;#f6v:E#\}Y/M%9zvlz\I[i", Hash = @"693E531209B3DEDF301C011FE1619A", },
                new PersonWithCredentialModel() { Id = 21, Type = PersonType.EM, FirstName = "Sariya-22", LastName = "Harnpadoungsataya", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "sharnpadoungsataya@adventure-works.com", Salt = @"@\;#f6v:E#\}Y/M%9zvlz\I[i", Hash = @"7655D4B26CEBAADBFC90D1124B9144", },
                new PersonWithCredentialModel() { Id = 22, Type = PersonType.EM, FirstName = "Mary-23", LastName = "Gibson", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "mgibson@adventure-works.com", Salt = @"@\;#f6v:E#\}Y/M%9zvlz\I[i", Hash = @"D8B647F3C182593F0C190465B0FCC5", },
                new PersonWithCredentialModel() { Id = 23, Type = PersonType.EM, Title = "Ms.", FirstName = "Jill-24", LastName = "Williams", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "jwilliams@adventure-works.com", Salt = @"@r0]h3^9 F<&.>@""xQlpV(Jq4", Hash = @"B47F65BB156E4560214A32E8051B73", },
                new PersonWithCredentialModel() { Id = 24, Type = PersonType.EM, FirstName = "James-25", LastName = "Hamilton", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "jhamilton@adventure-works.com", Salt = @"@r0]h3^9 F<&.>@""xQlpV(Jq4", Hash = @"8FF5E288E4A67AFD2D8967BA0064CD", },
                new PersonWithCredentialModel() { Id = 25, Type = PersonType.EM, FirstName = "Peter-26", LastName = "Krebs", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "pkrebs@adventure-works.com", Salt = @"@*$8k1E8Xiz-`L2}Y)ct3SK)]", Hash = @"D51CFC1F8198130B512892D3B8D7FA", },
                new PersonWithCredentialModel() { Id = 26, Type = PersonType.EM, FirstName = "Guy-28", LastName = "Gilbert", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "ggilbert@adventure-works.com", Salt = @"@(T'/-%U&xP7&`?yOpuy#-M&5", Hash = @"4AD2C786D45E8D87F7D90CB8131786", },
                new PersonWithCredentialModel() { Id = 27, Type = PersonType.EM, FirstName = "Mark-29", LastName = "McArthur", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "mmcarthur@adventure-works.com", Salt = @"@(T'/-%U&xP7&`?yOpuy#-M&5", Hash = @"80C670534D8BE216A63E5E3CE98BC4", },
                new PersonWithCredentialModel() { Id = 28, Type = PersonType.EM, FirstName = "Britta-30", LastName = "Simon", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "bsimon@adventure-works.com", Salt = @"@(T'/-%U&xP7&`?yOpuy#-M&5", Hash = @"D9E9C14BD0179BA8733E508129684E", },
                new PersonWithCredentialModel() { Id = 29, Type = PersonType.EM, FirstName = "Margie-31", LastName = "Shoop", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "mshoop@adventure-works.com", Salt = @"@?Ia1*kT^<0>Xo2v0Hl}^WN<^", Hash = @"D0DF5611BED9534842E9E3C8FAB2FA", },
                new PersonWithCredentialModel() { Id = 30, Type = PersonType.EM, FirstName = "Rebecca-32", LastName = "Laszlo", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "rlaszlo@adventure-works.com", Salt = @"@?Ia1*kT^<0>Xo2v0Hl}^WN<^", Hash = @"F097D372627065E487A7E54A40B710", },
                new PersonWithCredentialModel() { Id = 31, Type = PersonType.EM, FirstName = "Annik-33", LastName = "Stahl", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "astahl@adventure-works.com", Salt = @"@U>=3(RS9_nE, $so}b#:$OQ)", Hash = @"F4975F84A9D76D291C901687332ED4", },
                new PersonWithCredentialModel() { Id = 32, Type = PersonType.EM, FirstName = "Suchitra-34", LastName = "Mohan", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "smohan@adventure-works.com", Salt = @"@U>=3(RS9_nE, $so}b#:$OQ)", Hash = @"3FF5F858ED5935AC25E69280BBA9C8", },
                new PersonWithCredentialModel() { Id = 33, Type = PersonType.EM, FirstName = "Brandon-35", LastName = "Heidepriem", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "bheidepriem@adventure-works.com", Salt = @"@Sm,U$2qdnDNP42oegu(*\PO_", Hash = @"BA6B30212EC3F0FD8E008C4D9E34C1", },
                new PersonWithCredentialModel() { Id = 34, Type = PersonType.EM, FirstName = "Jose-36", LastName = "Lugo", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "jlugo@adventure-works.com", Salt = @"@Sm,U$2qdnDNP42oegu(*\PO_", Hash = @"A12801D06C7748EFA55528150BA8EF", },
                new PersonWithCredentialModel() { Id = 35, Type = PersonType.EM, FirstName = "Chris-37", LastName = "Okelberry", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "cokelberry@adventure-works.com", Salt = @"@ibfW""xp?3$U$B$lF>k+e(Qd*", Hash = @"AC95090BEB454CC8FFEFE1115C4CA9", },
                new PersonWithCredentialModel() { Id = 36, Type = PersonType.EM, FirstName = "Kim-38", LastName = "Abercrombie", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "kabercrombie@adventure-works.com", Salt = @"@ibfW""xp?3$U$B$lF>k+e(Qd*", Hash = @"F5CEEBDDC3E54B07B821C3F6B3FBAE", },
                new PersonWithCredentialModel() { Id = 37, Type = PersonType.EM, FirstName = "Ed-39", LastName = "Dudenhoefer", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "edudenhoefer@adventure-works.com", Salt = @"@""WBZ}`owUb\WQui'tb/ASRzR", Hash = @"32602E7D7111D5A1C8ECF353340AC1", },
                new PersonWithCredentialModel() { Id = 38, Type = PersonType.EM, FirstName = "JoLynn-40", LastName = "Dobney", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "jdobney@adventure-works.com", Salt = @"@""WBZ}`owUb\WQui'tb/ASRzR", Hash = @"804BAA95726B3E9D6F327CDB59B234", },
                new PersonWithCredentialModel() { Id = 39, Type = PersonType.EM, FirstName = "Bryan-41", LastName = "Baker", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "bbaker@adventure-works.com", Salt = @"@ )1|z?/Ed8fze$f{]t41-Sw+", Hash = @"AD57FFBA028D352DD9576678BDA4F3", },
                new PersonWithCredentialModel() { Id = 40, Type = PersonType.EM, FirstName = "James-42", LastName = "Kramer", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "jkramer@adventure-works.com", Salt = @"@ )1|z?/Ed8fze$f{]t41-Sw+", Hash = @"AB577A80E2B9C86D7E7A9670FD1D2E", },
                new PersonWithCredentialModel() { Id = 41, Type = PersonType.EM, FirstName = "Nancy-43", LastName = "Anderson", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "nanderson@adventure-works.com", Salt = @"@ )1|z?/Ed8fze$f{]t41-Sw+", Hash = @"FEEE7857A25D5C450006C5FAB4AA2E", },
                new PersonWithCredentialModel() { Id = 42, Type = PersonType.EM, FirstName = "Simon-44", LastName = "Rapier", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "srapier@adventure-works.com", Salt = @"Aw,KIlzH8BJ-MVYVRaaIOSYmH", Hash = @"62E7B2A6613A1FD0F6196F8CF8B613", },
                new PersonWithCredentialModel() { Id = 43, Type = PersonType.EM, FirstName = "Thomas-45", LastName = "Michaels", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "tmichaels@adventure-works.com", Salt = @"Av[:khYfdQ}6qjgRHKsN?-Zk ", Hash = @"97E86FAEA00B4C51C962F530F3A8B9", },
                new PersonWithCredentialModel() { Id = 44, Type = PersonType.EM, FirstName = "Eugene-46", LastName = "Kogan", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "ekogan@adventure-works.com", Salt = @"Av[:khYfdQ}6qjgRHKsN?-Zk ", Hash = @"76B94A166488DD6FE8F9AEB04C54B0", },
                new PersonWithCredentialModel() { Id = 45, Type = PersonType.EM, FirstName = "Andrew-47", LastName = "Hill", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "ahill@adventure-works.com", Salt = @"A.PsmfAe?s]=EyYO)""jQzX[""I", Hash = @"A8178AEFBB84EBF3A429FF0F25E6D2", },
                new PersonWithCredentialModel() { Id = 46, Type = PersonType.EM, FirstName = "Ruth-48", LastName = "Ellerbrock", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "rellerbrock@adventure-works.com", Salt = @"ADEOoc)dw8=Dx*KLhX`UV$\8r", Hash = @"E34D7EF97677E595C8B47CA5B9525E", },
                new PersonWithCredentialModel() { Id = 47, Type = PersonType.EM, FirstName = "Barry-49", LastName = "Johnson", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "bjohnson@adventure-works.com", Salt = @"ADEOoc)dw8=Dx*KLhX`UV$\8r", Hash = @"EE2008A2079AE9F00D4D3C8F4F97DF", },
                new PersonWithCredentialModel() { Id = 48, Type = PersonType.EM, FirstName = "Sidney-50", LastName = "Higa", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "shiga@adventure-works.com", Salt = @"ACu>3`g#EGqN==YH^AsZF\]5J", Hash = @"EA2E0926941A33E05EC20188713EBB", },
                new PersonWithCredentialModel() { Id = 49, Type = PersonType.EM, FirstName = "Jeffrey-51", LastName = "Ford", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "jford@adventure-works.com", Salt = @"ACu>3`g#EGqN==YH^AsZF\]5J", Hash = @"749D7E07BD59C237807AE5A9F2C8B9", },
                new PersonWithCredentialModel() { Id = 50, Type = PersonType.EM, FirstName = "Doris-52", LastName = "Hartwig", ModifiedDate = Convert.ToDateTime("2016-04-19 14:47:23"), Email = "dhartwig@adventure-works.com", Salt = @"ACu>3`g#EGqN==YH^AsZF\]5J", Hash = @"EF3EC9BB746C9815BA590DD26B048E", },
            };
            _maxPersonID = persons.Last().Id;
            _originalPersonsCount = persons.Count;

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
            repository.Setup(r => r.GetOneByValidCredential(It.IsAny<CredentialModel>())).Returns((CredentialModel c) => persons.Where(l => (l as PersonWithCredentialModel).Email == c.Login).SingleOrDefault());


            return repository;
        }

        private readonly IPersonDal _repository;

        public PersonDalTest()
        {
            _repository = PersonDalTest.GetMockedRepository().Object;
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
            Assert.AreEqual("Cracium", person.LastName);
        }

        [TestMethod]
        [TestCategory("PersonDal")]
        public void CanInsertPerson()
        {
            // Create a new person, not I do not supply an id
            PersonModel newPerson = new PersonModel() { Title = "Mr.", FirstName = "John", LastName = "Doe", };

            int personCount = _repository.SelectAll().Count;

            Assert.AreEqual(_originalPersonsCount, personCount); // Verify the expected Number pre-insert

            // Try saving our new person
            newPerson = this._repository.Insert(newPerson);
            Assert.IsNotNull(newPerson);

            // demand a recount
            personCount = _repository.SelectAll().Count;
            Assert.AreEqual(_originalPersonsCount + 1, personCount); // Verify the expected Number post-insert

            // verify that our new product has been saved
            PersonModel testPerson = _repository.SelectAll().OrderByDescending(x => x.Id).FirstOrDefault();

            Assert.IsNotNull(testPerson); // Test if null

            Assert.IsInstanceOfType(testPerson, typeof(PersonModel)); // Test type
            Assert.IsTrue(testPerson.Id > _maxPersonID); // Verify it has the expected personid
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
            testPerson = new PersonModel() { Id = int.MaxValue, Type = testPerson.Type, Title = testPerson.Title, FirstName = testPerson.FirstName, LastName = testPerson.LastName, };
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
        public void GetByCredentialReturnsCorrectLogin()
        {
            CredentialModel c = new CredentialModel() { Login = "ocracium@adventure-works.com", Password = "ocracium", };

            PersonModel person = _repository.GetOneByValidCredential(c);

            Assert.IsNotNull(person);
            Assert.AreEqual(person.FirstName, "Ovidiu-11");
            Assert.AreEqual(person.LastName, "Cracium");
        }

        #region Test Real Db

        [TestMethod]
        [TestCategory("PersonDal RealDb")]
        public void CanReturnAllPersonsFromDb()
        {
            PersonDal dal = new PersonDal(_connectionStringTestDb, _providerNameTestDb);
            IList<PersonModel> persons = dal.SelectAll();

            Assert.IsNotNull(persons);
            Assert.AreNotEqual(0, persons.Count);
            CollectionAssert.AllItemsAreUnique((System.Collections.ICollection)persons);
        }

        [TestMethod]
        [TestCategory("PersonDal RealDb")]
        public void SelectPasswordReturnCorrectLoginFromDb()
        {
            PersonDal dal = new PersonDal(_connectionStringTestDb, _providerNameTestDb);

            CredentialModel c = new CredentialModel() { Login = "ocracium@adventure-works.com", Password = "ocracium", };
            PersonModel person = dal.GetOneByValidCredential(c);

            Assert.IsNotNull(person);
            Assert.AreEqual(person.FirstName, "Ovidiu-11");
            Assert.AreEqual(person.LastName, "Cracium");
        }

        #endregion

    }
}
