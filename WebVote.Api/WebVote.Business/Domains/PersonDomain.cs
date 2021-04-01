using AutoMapper;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.ViewModels;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class PersonDomain : IPersonDomain
  {
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonDomain(
      IPersonRepository personRepository,
      IMapper mapper
      )
    {
      _mapper = mapper;
      _personRepository = personRepository;
    }

    public PersonInfoViewModel GetPersonInfo(int id)
      => _mapper.Map<PersonInfoViewModel>(_personRepository.GetById(id));
  }
}
