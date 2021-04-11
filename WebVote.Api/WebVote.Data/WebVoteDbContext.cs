using Microsoft.EntityFrameworkCore;
using WebVote.Data.Entities;

namespace WebVote.Data
{
  public class WebVoteDbContext : DbContext, IWebVoteDbContext
  {
    public WebVoteDbContext(DbContextOptions contextOptions) : base(contextOptions) { }

    public DbSet<Person> People { get; set; }
    public DbSet<PasswordCredentials> PasswordCredentials { get; set; }
    public DbSet<Poll> Polls { get; set; }
    public DbSet<PollOption> PollOptions { get; set; }
    public DbSet<VoterVote> VoterVotes { get; set; }

    public void Migrate()
    {
      this.Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      ConfigurePerson(modelBuilder);
      ConfigurePasswordCredentials(modelBuilder);
      ConfigurePoll(modelBuilder);
      ConfigurePollOptions(modelBuilder);
      ConfigureVoterVotes(modelBuilder);
    }

    private static void ConfigurePasswordCredentials(ModelBuilder modelBuilder)
    {
      var passwordCredentialsModelBuilder = modelBuilder.Entity<PasswordCredentials>();

      passwordCredentialsModelBuilder
        .HasKey(credentials => credentials.PersonId);

      passwordCredentialsModelBuilder
        .Property(credentials => credentials.Login)
        .IsRequired();

      passwordCredentialsModelBuilder
        .HasIndex(credentials => credentials.Login)
        .IsUnique();

      passwordCredentialsModelBuilder
        .Property(credentials => credentials.PasswordHash)
        .IsRequired();

      passwordCredentialsModelBuilder
        .Property(credentials => credentials.Salt)
        .IsRequired();

      passwordCredentialsModelBuilder
        .HasOne(credentials => credentials.Person)
        .WithOne(person => person.PasswordCredentials)
        .HasForeignKey<PasswordCredentials>(credentials => credentials.PersonId);
    }

    private static void ConfigurePerson(ModelBuilder modelBuilder)
    {
      var personModelBuilder = modelBuilder.Entity<Person>();

      personModelBuilder
        .HasKey(person => person.Id);

      personModelBuilder
        .Property(person => person.FullName)
        .IsRequired();

      personModelBuilder
        .Property(person => person.Birth)
        .IsRequired();

      personModelBuilder
        .Property(person => person.Role)
        .IsRequired();

      personModelBuilder
        .Property(person => person.IndividualTaxNumber)
        .IsRequired();

      personModelBuilder
        .HasIndex(person => person.IndividualTaxNumber)
        .IsUnique();
    }

    private static void ConfigurePoll(ModelBuilder modelBuilder)
    {
      var pollModelBuilder = modelBuilder.Entity<Poll>();

      pollModelBuilder
        .HasKey(poll => poll.Id);

      pollModelBuilder
        .Property(poll => poll.Title)
        .IsRequired();

      pollModelBuilder
        .Property(poll => poll.Description)
        .IsRequired();
    }

    private static void ConfigurePollOptions(ModelBuilder modelBuilder)
    {
      var pollOptionModelBuilder = modelBuilder.Entity<PollOption>();

      pollOptionModelBuilder
        .HasKey(pollOption => pollOption.Id);

      pollOptionModelBuilder
        .Property(pollOption => pollOption.Title)
        .IsRequired();

      pollOptionModelBuilder
        .Property(pollOption => pollOption.Description)
        .IsRequired();

      pollOptionModelBuilder
        .HasOne(x => x.Poll)
        .WithMany(x => x.Options)
        .HasForeignKey(x => x.PollId);
    }

    private static void ConfigureVoterVotes(ModelBuilder modelBuilder)
    {
      var voterVoteModelBuilder = modelBuilder.Entity<VoterVote>();

      voterVoteModelBuilder
        .HasKey(voterVote => new { voterVote.PersonId, voterVote.PollId });

      voterVoteModelBuilder
        .HasOne(voterVote => voterVote.Person)
        .WithMany(person => person.Votes)
        .HasForeignKey(voterVote => voterVote.PersonId);

      voterVoteModelBuilder
        .HasOne(voterVote => voterVote.Poll)
        .WithMany(poll => poll.Votes)
        .HasForeignKey(voterVote => voterVote.PollId);

      voterVoteModelBuilder
        .HasOne(voterVote => voterVote.PollOption)
        .WithMany(option => option.Votes)
        .HasForeignKey(voterVote => voterVote.PollOptionId);
    }
  }
}
