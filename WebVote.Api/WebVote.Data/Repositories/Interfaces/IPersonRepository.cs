using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IPersonRepository
  {
    Person GetById(int id);
    Person GetByITN(string itn);
    Person Create(Person person);
  }
}
