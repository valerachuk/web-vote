using System.Linq;
using Microsoft.EntityFrameworkCore;
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

    public Person ReadPerson(int id) =>
      _context.People.First(person => person.Id == id);


    public Person ReadPersonWithRegionAndCredentials(int id) =>
      _context.People
        .Include(person => person.Region)
        .Include(person => person.PasswordCredentials)
        .First(person => person.Id == id);

    public Person ReadPersonByITN(string itn) =>
      _context.People.FirstOrDefault(person => person.IndividualTaxNumber == itn);

    public Person Create(Person person)
    {
      _context.People.Add(person);
      _context.SaveChanges();
      return person;
    }
  }
}
