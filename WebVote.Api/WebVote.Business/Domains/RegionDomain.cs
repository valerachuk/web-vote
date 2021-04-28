using System.Collections.Generic;
using AutoMapper;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.RESTResponses;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class RegionDomain : IRegionDomain
  {
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionDomain(
      IRegionRepository regionRepository,
      IMapper mapper
      )
    {
      _mapper = mapper;
      _regionRepository = regionRepository;
    }

    public IList<RegionResponse> GetRegions()
    {
      return _mapper.Map<IList<RegionResponse>>(_regionRepository.ReadRegions());
    }

  }
}
