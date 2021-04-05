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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      ConfigurePerson(modelBuilder);
      ConfigurePasswordCredentials(modelBuilder);
      ConfigurePoll(modelBuilder);
      ConfigurePollOptions(modelBuilder);
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
        .HasForeignKey<PasswordCredentials>(credentials => credentials.PersonId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
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
        .Property(pollOption => pollOption.Order)
        .IsRequired();

      pollOptionModelBuilder
        .HasOne(x => x.Poll)
        .WithMany(x => x.Options)
        .HasForeignKey(x => x.PollId)
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired();
    }
  }
}
