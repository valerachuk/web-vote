using Microsoft.EntityFrameworkCore;
using WebVote.Data.Entities;

namespace WebVote.Data
{
  public class WebVoteDbContext : DbContext, IWebVoteDbContext
  {
    public WebVoteDbContext(DbContextOptions contextOptions) : base(contextOptions) { }

    public DbSet<Person> People { get; set; }
    public DbSet<PasswordCredentials> PasswordCredentials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      #region Person
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
      #endregion

      #region PasswordCredentials
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
        .OnDelete(DeleteBehavior.Cascade);
      #endregion
    }
  }
}
