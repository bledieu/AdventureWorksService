using AdventureWorks.Dal;
using AdventureWorks.Dal.Tests;
using Moq;

namespace AdventureWorks.UnitTests.Person
{
    public abstract class AServicePersonTest
    {
        protected internal readonly Mock<IPersonDal> _mockedRepository;
        protected internal readonly IPersonDal _repository;

        public AServicePersonTest()
        {
            _mockedRepository = PersonDalTest.GetMockedRepository();
            _repository = _mockedRepository.Object;
        }
    }
}
