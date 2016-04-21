using AdventureWorks.Model;

namespace AdventureWorks.Dal
{
    public interface IPersonDal : IRepositoryT<PersonModel>
    {
        PersonModel GetOneByValidCredential(CredentialModel credential);
    }
}
