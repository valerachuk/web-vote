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
      var pollOptionVotesCountDtos = _analyticRepository.ReadNumberOfVotesPerOption(pollId);

      var votesCount = pollOptionVotesCountDtos.Sum(pollOptionVotesCount => pollOptionVotesCount.VotesCount);

      return pollOptionVotesCountDtos.Select(pollOptionVotesCountDTO =>
      {
        var votesPerOption = _mapper.Map<VotesPerOptionResponse>(pollOptionVotesCountDTO);
        votesPerOption.Percent = votesCount != 0 ? 1m * pollOptionVotesCountDTO.VotesCount / votesCount : 0m;

        return votesPerOption;
      });
    }

    public (byte[], string) GetVotesPerOptionCSV(int pollId)
    {
      var fileName = CreatePollFileNameCSV(pollId, "votes per option");

      var votesPerOption = GetVotesPerOption(pollId);

      return (ToCSV(votesPerOption), fileName);
    }

    public IEnumerable<VotesPerRegionResponse> GetVotesPerRegion(int pollId)
    {
      var regionsCitizensVotesCountDtos = _analyticRepository.ReadNumberOfVotesPerRegion(pollId);

      var votesCount = regionsCitizensVotesCountDtos.Sum(regionCitizensVotesCountDto => regionCitizensVotesCountDto.VotesCount);

      return regionsCitizensVotesCountDtos.Select(regionCitizensVotesCountDto =>
      {
        var votesPerRegion = _mapper.Map<VotesPerRegionResponse>(regionCitizensVotesCountDto);

        votesPerRegion.VotersActivityPercent = regionCitizensVotesCountDto.CitizensCount != 0
          ? 1m * regionCitizensVotesCountDto.VotesCount / regionCitizensVotesCountDto.CitizensCount
          : 0m;

        votesPerRegion.VotesPercent = votesCount != 0 ? 1m * regionCitizensVotesCountDto.VotesCount / votesCount : 0m;

        return votesPerRegion;
      }).OrderByDescending(votesPerRegion => votesPerRegion.VotersActivityPercent);
    }

    public (byte[], string) GetVotesPerRegionCSV(int pollId)
    {
      var fileName = CreatePollFileNameCSV(pollId, "votes per region");

      var votesPerRegion = GetVotesPerRegion(pollId);

      return (ToCSV(votesPerRegion), fileName);
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
