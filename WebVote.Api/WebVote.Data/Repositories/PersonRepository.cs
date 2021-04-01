using System.Linq;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Data.Repositories
{
  public class PersonRepository : IPersonRepository
  {
    private readonly IWebVoteDbContext _context;

    public PersonRepository(IWebVoteDbContext context)
    {
      _context = context;
    }

    public Person GetById(int id) =>
      _context.People.First(person => person.Id == id);

    public Person GetByITN(string itn) =>
      _context.People.FirstOrDefault(person => person.IndividualTaxNumber == itn);

    public Person Create(Person person)
    {
      _context.People.Add(person);
      _context.SaveChanges();
      return person;
    }
  }
}
