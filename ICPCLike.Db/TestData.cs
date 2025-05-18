using ICPCLike.Db.Context;
using ICPCLike.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace ICPCLike.Db;

public static class TestData
{
	private static readonly Random _rand = new();

	public static void Seed(IcpcLikeContext context)
	{
		context.Database.Migrate();

		if (context.Persons.Any()) return;

		var countries = new[] { "Ukraine", "Poland", "Germany", "USA", "Canada" };
		var orgs = new List<Organization>();
		for (var i = 1; i <= 20; i++)
		{
			orgs.Add(new Organization
			{
				Name = $"University #{i}",
				Country = countries[_rand.Next(countries.Length)]
			});
		}
		context.Organizations.AddRange(orgs);

		var persons = new List<Person>();
		for (var i = 1; i <= 100; i++)
		{
			var role = i <= 10 ? Role.Coach : i <= 20 ? Role.Reserve : Role.Contestant;
			persons.Add(new Person
			{
				IcpcId = $"ICPC{i:0000}",
				Name = $"Person {i}",
				Email = $"person{i}@example.com",
				Sex = i % 2 == 0 ? "M" : "F",
				Title = role == Role.Coach ? "Dr." : null,
				Role = role,
				DateOfBirth = DateTime.UtcNow.AddYears(-18 - _rand.Next(7)).Date
			});
		}
		context.Persons.AddRange(persons);

		var seasons = new List<Season>();
		for (var i = 0; i < 4; i++)
		{
			seasons.Add(new Season
			{
				Name = $"Season {2020 + i}",
				StartDate = DateTime.SpecifyKind(new DateTime(2020 + i, 1, 1), DateTimeKind.Utc),
				EndDate = DateTime.SpecifyKind(new DateTime(2020 + i, 12, 31), DateTimeKind.Utc)
			});
		}
		context.Seasons.AddRange(seasons);

		var stages = new List<Stage>();
		foreach (var season in seasons)
		{
			for (var j = 1; j <= 3; j++)
			{
				stages.Add(new Stage
				{
					Name = $"Stage {j} of {season.Name}",
					Date = DateTime.SpecifyKind(season.StartDate.AddMonths(j * 2), DateTimeKind.Utc),
					Level = (Level)(j % 2),
					Season = season
				});
			}
		}
		context.Stages.AddRange(stages);

		var teams = new List<Team>();
		var teamMembers = new List<TeamMember>();
		var results = new List<Result>();
		var substitutions = new List<Substitution>();

		var contestants = persons.Where(p => p.Role == Role.Contestant).ToList();
		var teamCounter = 1;
		for (var i = 0; i < 30; i++)
		{
			var team = new Team
			{
				Name = $"Team #{teamCounter++}",
				Organization = orgs[_rand.Next(orgs.Count)],
				Hidden = i % 7 == 0
			};
			teams.Add(team);

			var members = contestants.OrderBy(_ => _rand.Next()).Take(3).ToList();
			foreach (var m in members)
			{
				teamMembers.Add(new TeamMember
				{
					Team = team,
					Contestant = m,
					JoinDate = DateTime.UtcNow.AddMonths(-_rand.Next(12)).Date,
					LeaveDate = _rand.NextDouble() < 0.3 ? DateTime.UtcNow.Date : null
				});
			}
		}
		context.Teams.AddRange(teams);
		context.TeamMembers.AddRange(teamMembers);

		foreach (var stage in stages)
		{
			var stageTeams = teams.OrderBy(_ => _rand.Next()).Take(10).ToList();
			var rank = 1;
			foreach (var t in stageTeams)
			{
				results.Add(new Result
				{
					Stage = stage,
					Team = t,
					Rank = rank++,
					SolvedTasks = _rand.Next(1, 10),
					PenaltyTime = _rand.Next(100, 1500)
				});
			}
		}
		context.Results.AddRange(results);

		for (var i = 0; i < 10; i++)
		{
			var team = teams[_rand.Next(teams.Count)];
			var oldMember = contestants[_rand.Next(contestants.Count)];
			var newMember = contestants.Except(new[] { oldMember }).OrderBy(_ => _rand.Next()).First();
			substitutions.Add(new Substitution
			{
				Team = team,
				OldContestant = oldMember,
				NewContestant = newMember,
				SubstitutionDate = DateTime.UtcNow.AddDays(-_rand.Next(200)).Date
			});
		}
		context.Substitutions.AddRange(substitutions);

		context.SaveChanges();
	}
}