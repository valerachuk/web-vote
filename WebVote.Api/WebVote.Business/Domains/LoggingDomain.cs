using System.Collections.Generic;
using AutoMapper;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.RESTResponses;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class LoggingDomain : ILoggingDomain
  {
    private readonly IRegistrationLogRepository _registrationLogRepository;
    private readonly IMapper _mapper;

    public LoggingDomain(
      IRegistrationLogRepository registrationLogRepository,
      IMapper mapper
    )
    {
      _mapper = mapper;
      _registrationLogRepository = registrationLogRepository;
    }

    public IEnumerable<RegistrationLogRecordResponse> GetRegistrationLogOrderedByTimestamp()
    {
      var registrationLog = _registrationLogRepository.ReadOrderedByTimestamp();
      return _mapper.Map<IEnumerable<RegistrationLogRecordResponse>>(registrationLog);
    }
  }
}
