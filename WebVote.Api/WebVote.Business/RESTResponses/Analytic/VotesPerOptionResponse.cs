namespace WebVote.Business.RESTResponses.Analytic
{
  public class VotesPerOptionResponse
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int Count { get; set; }
    public decimal Percent { get; set; }
  }
}
