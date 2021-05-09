using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CsvHelper;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.RESTResponses.Analytic;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class AnalyticDomain : IAnalyticDomain
  {
    private readonly IAnalyticRepository _analyticRepository;
    private readonly IPollRepository _pollRepository;
    private readonly IDateProviderDomain _dateProvider;
    private readonly IMapper _mapper;

    public AnalyticDomain(
      IAnalyticRepository analyticRepository,
      IPollRepository pollRepository,
      IDateProviderDomain dateProvider,
      IMapper mapper
      )
    {
      _mapper = mapper;
      _pollRepository = pollRepository;
      _analyticRepository = analyticRepository;
      _dateProvider = dateProvider;
    }

    public IEnumerable<VotesPerOptionResponse> GetVotesPerOption(int pollId)
    {
      var pollOptionVotes = _analyticRepository.ReadNumberOfVotesPerOption(pollId);

      var votesCount = pollOptionVotes.Select(tuple => tuple.Item2).Sum();
      var pollOptionVotesPercent = pollOptionVotes
        .Select(tuple => ValueTuple.Create(tuple.Item1, tuple.Item2, votesCount != 0 ? 1m * tuple.Item2 / votesCount : 0m));

      return _mapper.Map<IEnumerable<VotesPerOptionResponse>>(pollOptionVotesPercent);
    }

    public (byte[], string) GetVotesPerOptionCSV(int pollId)
    {
      var fileName = CreatePollFileNameCSV(pollId, "votes per option");

      var percentOfVotesPerOption = GetVotesPerOption(pollId);

      return (ToCSV(percentOfVotesPerOption), fileName);
    }

    private static byte[] ToCSV<T>(IEnumerable<T> records)
    {
      using var memoryStream = new MemoryStream();

      using (var streamWriter = new StreamWriter(memoryStream))
      using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
      {
        csvWriter.WriteRecords(records);
      }

      return memoryStream.ToArray();
    }

    private string CreatePollFileNameCSV(int pollId, string contentDescription)
    {
      var poll = _pollRepository.ReadPoll(pollId);
      return $"{poll.Title}_{contentDescription}_{_dateProvider.UtcNow:yyyy-MM-ddTHH.mm.ssZ}.csv";
    }

  }
}
