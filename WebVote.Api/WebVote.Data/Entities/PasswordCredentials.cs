namespace WebVote.Data.Entities
{
  public class PasswordCredentials
  {
    public int PersonId { get; set; }
    public Person Person { get; set; }

    public string Login { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] Salt { get; set; }
  }
}
