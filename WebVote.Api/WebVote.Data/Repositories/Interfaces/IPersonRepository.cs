using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IPersonRepository
  {
    Person ReadPerson(int id);
    Person ReadPersonWithRegionAndCredentials(int id);
    Person ReadPersonByITN(string itn);
    Person Create(Person person);
  }
}
