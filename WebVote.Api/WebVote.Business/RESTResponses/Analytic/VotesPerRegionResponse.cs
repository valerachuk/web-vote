namespace WebVote.Business.RESTResponses.Analytic
{
  public class VotesPerRegionResponse
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public int VotesCount { get; set; }
    public int CitizensCount { get; set; }
    public decimal VotersActivityPercent { get; set; }
    public decimal VotesPercent { get; set; }
  }
}
