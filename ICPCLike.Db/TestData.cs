using ICPCLike.Db.Context;
using ICPCLike.Db.Models;

namespace ICPCLike.Db;

public static class TestData
{
	public static void Seed(IcpcLikeContext context)
	{
		var random = new Random();
		
		if (!context.Organizations.Any())
        {
	        var organizations = new Organization[15];

	        for (var i = 0; i < 15; i++)
	        {
		        organizations[i] = new Organization
		        {
			        Name = $"Organizatoin {i + 1}",
			        Country = $"Country {i + 1}"
		        };
	        }
	        
            context.Organizations.AddRange(organizations);
            context.SaveChanges();
        }
		
		if (!context.Persons.Any())
		{
			const int totalCoaches = 5;
			const int totalContestants = 45;
			
		    var coaches = new List<Person>();
		    for (var i = 0; i < totalCoaches; i++)
		    {
		        coaches.Add(new Person
		        {
		            IcpcId = $"C00{i + 1}",
		            Name = $"Coach {i + 1}",
		            Title = "Mr.",
		            Email = $"coach.{i + 1}@example.com",
		            Sex = "M",
		            Role = Role.Coach,
		            DateOfBirth = DateTime.UtcNow.AddYears(-random.Next(30, 50)).AddDays(-random.Next(0, 365))
		        });
		    }
		    
		    var contestants = new List<Person>();
		    for (var i = 0; i < totalContestants; i++)
		    {
		        contestants.Add(new Person
		        {
		            IcpcId = $"P00{i + 1}",
		            Name = $"Person name {i + 1}",
		            Title = i % 2 == 0 ? "Mr." : "Ms.",
		            Email = $"seed.{i + 1}@example.com",
		            Sex = i % 2 == 0 ? "M" : "F",
		            Role = Role.Contestant,
		            DateOfBirth = DateTime.UtcNow.AddYears(-random.Next(16, 31)).AddDays(-random.Next(0, 365))
		        });
		    }
		    
		    context.Persons.AddRange(coaches);
		    context.Persons.AddRange(contestants);
		    context.SaveChanges();
		}
		
        if (!context.Teams.Any())
		{
		    var organizations = context.Organizations.ToList();
		    var coaches = context.Persons.Where(p => p.Role == Role.Coach).ToList();
		    var contestants = context.Persons.Where(p => p.Role == Role.Contestant).ToList();
		    
		    const int totalTeams = 15;

		    var teams = new List<Team>();
		    
		    for (var i = 0; i < totalTeams; i++)
		    {
		        var team = new Team
		        {
		            Name = $"Team {i + 1}",
		            OrganizationId = organizations.Count > 0 ? organizations[random.Next(organizations.Count)].Id : null,
		            Hidden = i % 5 == 0
		        };
		        
		        var coach = coaches.FirstOrDefault();
		        if (coach != null)
		        {
		            var teamMemberCoach = new TeamMember
		            {
		                Team = team,
		                ContestantId = coach.Id,
		                Role = Role.Coach,
		                JoinDate = DateTime.UtcNow.AddDays(-random.Next(100, 300)),
		                LeaveDate = null
		            };
		            
		            team.Members.Add(teamMemberCoach);
		            coaches.Remove(coach);
		        }
		        
		        var teamContestants = contestants.OrderBy(c => random.Next()).Take(3).ToList();
		        foreach (var contestant in teamContestants)
		        {
		            var teamMemberContestant = new TeamMember
		            {
		                Team = team,
		                ContestantId = contestant.Id,
		                Role = Role.Contestant,
		                JoinDate = DateTime.UtcNow.AddDays(-random.Next(100, 300)),
		                LeaveDate = null
		            };
		            context.TeamMembers.Add(teamMemberContestant);
		            team.Members.Add(teamMemberContestant);
		            contestants.Remove(contestant);
		        }
		        
		        teams.Add(team);
		    }
		    
		    context.Teams.AddRange(teams);
		    context.SaveChanges();
		}
        
        if (!context.Seasons.Any())
        {
			var seasons = new Season[15];

			for (var i = 0; i < 15; i++)
			{
				var startYear = 2010 + i;
				var startDate = new DateTime(startYear, 9, 1);
				var endDate = new DateTime(startYear + 1, 6, 30);

				seasons[i] = new Season
				{
					Name = $"{startYear}/{startYear + 1} Season",
					StartDate = startDate,
					EndDate = endDate
				};
			}

			context.Seasons.AddRange(seasons);
			context.SaveChanges();
        }
        
        if (!context.Stages.Any())
		{
			var stages = new List<Stage>();
			var seasons = context.Seasons.ToList();

			foreach (var season in seasons)
			{
				stages.Add(new Stage
				{
					Name = $"{season.Name} - Regional",
					Date = season.StartDate.AddMonths(2),
					Level = Level.Regional,
					SeasonId = season.Id
				});

				stages.Add(new Stage
				{
					Name = $"{season.Name} - National",
					Date = season.EndDate.AddMonths(-2),
					Level = Level.National,
					SeasonId = season.Id
				});
			}

			context.Stages.AddRange(stages);
			context.SaveChanges();
		}
        
        if (!context.Results.Any())
        {
			var results = new List<Result>();
			var stages = context.Stages.ToList();
			var teams = context.Teams.ToList();

			foreach (var stage in stages)
			{
				var participatingTeams = teams.OrderBy(_ => random.Next()).Take(5).ToList();

				for (var i = 0; i < participatingTeams.Count; i++)
				{
					results.Add(new Result
					{
						StageId = stage.Id,
						TeamId = participatingTeams[i].Id,
						Rank = i + 1,
						SolvedTasks = random.Next(3, 13),
						PenaltyTime = random.Next(300, 2000)
					});
				}

				context.Results.AddRange(results); 
				context.SaveChanges();
			}
        }
        
        if (!context.Substitutions.Any())
		{
			var teams = context.Teams.ToList();
			var contestants = context.Persons.Where(p => p.Role == Role.Contestant).ToList();
			var substitutions = new List<Substitution>();

			for (var i = 0; i < 5; i++)
			{
				var team = teams[random.Next(teams.Count)];

				var oldContestant = contestants[random.Next(contestants.Count)];
				Person newContestant;
				do
				{
					newContestant = contestants[random.Next(contestants.Count)];
				} while (newContestant.Id == oldContestant.Id);

				substitutions.Add(new Substitution
				{
					TeamId = team.Id,
					OldContestantId = oldContestant.Id,
					NewContestantId = newContestant.Id,
					SubstitutionDate = DateTime.UtcNow.AddDays(-random.Next(1, 100))
				});
			}

			context.Substitutions.AddRange(substitutions);
			context.SaveChanges();
		}

        
        if (!context.TeamMembers.Any())
		{
			var teams = context.Teams.ToList();
			var contestants = context.Persons.Where(p => p.Role == Role.Contestant).ToList();

			var teamMembers = new List<TeamMember>();
			var usedPairs = new HashSet<(int TeamId, int ContestantId)>();

			for (var i = 0; i < 15; i++)
			{
				var team = teams[random.Next(teams.Count)];
				var contestant = contestants[random.Next(contestants.Count)];
				
				if (!usedPairs.Add((team.Id, contestant.Id)))
				{
					i--;
					continue;
				}

				var joinDate = DateTime.UtcNow.AddDays(-random.Next(200, 1000));
				DateTime? leaveDate = random.Next(0, 3) == 0 ? joinDate.AddDays(random.Next(30, 300)) : null;

				teamMembers.Add(new TeamMember
				{
					TeamId = team.Id,
					ContestantId = contestant.Id,
					JoinDate = joinDate,
					LeaveDate = leaveDate
				});
			}

			context.TeamMembers.AddRange(teamMembers);
			context.SaveChanges();
		}
	}
}