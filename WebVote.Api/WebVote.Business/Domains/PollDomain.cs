using AutoMapper;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.ViewModels;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class PollDomain : IPollDomain
  {
    private readonly IPollRepository _pollRepository;
    private readonly IMapper _mapper;

    public PollDomain(
      IPollRepository pollRepository,
      IMapper mapper
      )
    {
      _mapper = mapper;
      _pollRepository = pollRepository;
    }

    public void CreatePool(PollViewModel pollViewModel)
    {
      var poll = _mapper.Map<Poll>(pollViewModel);
      _pollRepository.Create(poll);
    }
  }
}
