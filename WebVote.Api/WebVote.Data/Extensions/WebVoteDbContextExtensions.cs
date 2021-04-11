using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebVote.Constants;
using WebVote.Data.Entities;

namespace WebVote.Data.Extensions
{
  public static class WebVoteDbContextExtensions
  {
    public static void EnsureSeeded(this IWebVoteDbContext webVoteDbContext)
    {
      if (
        webVoteDbContext.Polls.Any() ||
        webVoteDbContext.PollOptions.Any() ||
        webVoteDbContext.People.Any() ||
        webVoteDbContext.VoterVotes.Any() ||
        webVoteDbContext.PasswordCredentials.Any())
      {
        return;
      }

      var people = GetDummyPeople();
      var polls = GetDummyPolls();
      var votes = GetDummyVotes(people, polls);

      var admin = CreateUserWithPasswordCredentials("a", "3ZcKTC3gq33SPWTF", UserRoles.ADMIN);
      var manager = CreateUserWithPasswordCredentials("m", "VeaNCa2FZCgB7cQq", UserRoles.MANAGER);
      var voter = CreateUserWithPasswordCredentials("v", "7EAhtdHKqauCXQwU", UserRoles.VOTER);

      webVoteDbContext.People.AddRange(new[] { admin, manager, voter });
      webVoteDbContext.People.AddRange(people);
      webVoteDbContext.Polls.AddRange(polls);
      webVoteDbContext.VoterVotes.AddRange(votes);
      webVoteDbContext.SaveChanges();
    }

    public static Person CreateUserWithPasswordCredentials(string login, string password, string role)
    {
      var sha256 = SHA256.Create();
      var passwordCredentials = new PasswordCredentials
      {
        Login = login,
        Salt = new byte[0],
        PasswordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
      };

      return new Person
      {
        Birth = new DateTime(1990, 1, 1),
        FullName = Guid.NewGuid().ToString(),
        IndividualTaxNumber = Guid.NewGuid().ToString(),
        Role = role,
        PasswordCredentials = passwordCredentials
      };
    }

    private static IEnumerable<VoterVote> GetDummyVotes(IList<Person> people, IList<Poll> polls)
    {
      var rnd = new Random();
      IEnumerable<VoterVote> GenerateRandomVotes(IList<PollOption> options, Poll poll)
      {
        var optionsLength = options.Count();
        return people.Select((person, idx) => new VoterVote
        {
          Person = person,
          Poll = poll,
          PollOption = options.ElementAt(rnd.Next(optionsLength))
        });
      }

      return polls.SelectMany(poll => GenerateRandomVotes(poll.Options, poll)).ToList();
    }

    private static IList<Person> GetDummyPeople()
    {
      var rnd = new Random();
      var startDate = new DateTime(1240, 1, 1);
      var endDate = new DateTime(2000, 1, 1);
      var rangeDays = (endDate - startDate).Days;

      return Enumerable.Range(0, 200).Select(i => new Person
      {
        IndividualTaxNumber = Guid.NewGuid().ToString(),
        FullName = Guid.NewGuid().ToString(),
        Role = UserRoles.VOTER,
        Birth = startDate.AddDays(rnd.Next(rangeDays))
      }).ToList();
    }

    private static IList<Poll> GetDummyPolls()
    {
      var poll1 = new Poll
      {
        Title = "Choose your favorite SpongeBob SquarePants character",
        Description = "SpongeBob SquarePants (also simply referred to as SpongeBob) is an American animated comedy television series created by marine science educator and animator Stephen Hillenburg for Nickelodeon. The series chronicles the adventures and endeavors of the title character and his aquatic friends in the fictional underwater city of Bikini Bottom. The fifth-longest-running American animated series, its popularity has made it a media franchise. It is the highest rated series to air on Nickelodeon and it is the most distributed property from ViacomCBS Networks International. The media franchise has generated more than $13 billion in merchandising revenue for Nickelodeon.[6][needs update]",
        Options = new[]
        {
          new PollOption
          {
            Title = "SpongeBob SquarePants",
            Description = "SpongeBob SquarePants (voiced by Tom Kenny) is a yellow anthropomorphic sea sponge who usually wears brown short pants, a white collared shirt, and a red tie. He lives in a pineapple house and is employed as a fry cook at a fast food restaurant called the Krusty Krab.[16] He diligently attends Mrs. Puff's Boating School but has never passed; throughout the series, he tries his hardest on the exams but remains an unintentionally reckless boat driver. He is relentlessly optimistic and enthusiastic toward his job and his friends. SpongeBob's hobbies include catching jellyfish, blowing bubbles, playing with his best friend Patrick, and unintentionally irritating his neighbor Squidward. He first appears in \"Help Wanted\".[17]"
          },
          new PollOption
          {
            Title = "Patrick Star",
            Description = "Patrick Star (voiced by Bill Fagerbakke, Jack Gore as young Patrick in The SpongeBob Movie: Sponge on the Run) a pink starfish who lives under a rock and wears flowered swim trunks. His most prominent character trait is his extremely low intelligence. He is best friends with SpongeBob and often unknowingly encourages activities that get the two into trouble.[16] While typically unemployed throughout the series, Patrick holds various short-term jobs as the storyline of each episode requires. He is generally slow and easy-going but can sometimes get aggressive, much like real starfish, and occasionally performs feats of great strength.[18]"
          },
          new PollOption
          {
            Title = "Squidward Tentacles",
            Description = "Squidward Tentacles (voiced by Rodger Bumpass, Jason Maybaum as young Squidward in The SpongeBob Movie: Sponge on the Run) an octopus with a large nose who works as a cashier at the Krusty Krab. He is SpongeBob's next-door neighbor with a dry, sarcastic sense of humor. His house is between the SpongeBob and Patrick houses.[19] He believes himself to be a talented artist and musician, but nobody else recognizes his abilities. He plays the clarinet and often paints self-portraits in different styles, which he hangs up around his moai house. Squidward frequently voices his frustration with SpongeBob, but he genuinely cares for him deep down. This has been revealed in the form of sudden confessions when Squidward is in a dire situation."
          },
          new PollOption
          {
            Title = "Mr. Krabs",
            Description = "Eugene Krabs (voiced by Clancy Brown) is a red crab who lives in an anchor-shaped house with his daughter Pearl, who is a whale. He dislikes spending money but will go to great lengths to make Pearl happy.[20] Krabs owns and operates the Krusty Krab restaurant where SpongeBob works. He is self-content, cunning, and obsessed with the value and essence of money.[16] He tends to worry more about his riches than about the needs of his employees. Having served in the navy, he loves sailing, whales, sea shanties, and talking like a pirate."
          },
          new PollOption
          {
            Title = "Plankton and Karen",
            Description = "Sheldon Plankton (voiced by Mr. Lawrence) and Karen Plankton (voiced by Jill Talley) are the owners of the Chum Bucket, an unsuccessful restaurant located across the street from the Krusty Krab. Their business is a commercial failure because they sell mostly inedible foods made from chum. Plankton is a small planktonic copepod[21] and the self-proclaimed archenemy of Mr. Krabs. His ultimate goal is to steal Krabs' secret formula for Krabby Patties, run the Krusty Krab out of business and take over the oceanic world but never permanently succeeds due to either SpongeBob and/or Krabs' efforts, his own incompetence and childish nature, or his own small size (except, temporarily, in The SpongeBob SquarePants Movie). He is a skilled inventor and possesses a Napoleon complex due to his short stature.[22] Karen is Plankton's own invention, a waterproof supercomputer[23] who is more competent that Plankton, being the brains behind most of his evil plans to steal Krabs' secret recipe.[24] She is married to Plankton and usually takes residence in the Chum Bucket laboratory."
          },
        },

      };

      var poll2 = new Poll
      {
        Title = "Голосование за любимого главного персонажа из «Лунтик и его друзья»",
        Description = "«Лунтик и его друзья» (ранее назывался «Приключения Лунтика и его друзей») — российский мультсериал, ориентированный на семейную и детскую аудиторию. Транслируется на телевидении с 1 сентября 2006 года по настоящее время. Ключевой темой стали приключения маленького пушистого существа Лунтика — космического пришельца, который родился на Луне.",
        Options = new[]
        {
          new PollOption
          {
            Title = "Лунтик",
            Description = "Лунтик — основной персонаж мультсериала, который упал с луны. Он добрый и справедливый, всегда всем помогает."
          },
          new PollOption
          {
            Title = "Луна",
            Description = "Луна - также упала с луны. Подруга Лунтика."
          },
          new PollOption
          {
            Title = "Кузя",
            Description = "Кузя — озорной зеленый кузнечик, друг Лунтика."
          },
          new PollOption
          {
            Title = "Мила",
            Description = "Мила — божья коровка, подруга Лунтика."
          },
          new PollOption
          {
            Title = "Пчелёнок",
            Description = "Пчелёнок — ученик Пчелиной школы, тоже дружит с Лунтиком."
          },
          new PollOption
          {
            Title = "Вупсень и Пупсень",
            Description = "Вупсень и Пупсень — гусеницы, были довольно вредными персонажами до седьмого сезона."
          },
          new PollOption
          {
            Title = "Баба Капа",
            Description = "Баба Капа — приёмная бабушка Лунтика, добрая пчела."
          },
          new PollOption
          {
            Title = "Дед Шер",
            Description = "Дед Шер — приёмный дедушка Лунтика, генерал в отставке."
          },
          new PollOption
          {
            Title = "Корней Корнеевич",
            Description = "Корней Корнеевич — червяк, мастер на все руки."
          },
          new PollOption
          {
            Title = "Дядя Шнюк ",
            Description = "Дядя Шнюк — паук, занимается разнообразным творчеством."
          },
        }
      };

      var poll3 = new Poll
      {
        Title = "Референдум щодо поправок до Конституції України",
        Description = "Ви схвалюєте зміни до Конституції України?",
        Options = new[]
        {
          new PollOption
          {
            Title = "Так",
            Description = "Так, схвалюю"
          },
          new PollOption
          {
            Title = "Ні",
            Description = "Ні, не схвалюю"
          },
        }
      };

      return new[] { poll1, poll2, poll3 };

    }
  }
}
