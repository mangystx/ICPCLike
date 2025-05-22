using ICPCLike.Db.Context;
using ICPCLike.Db.Models;
using ICPCLike.DTO;
using Microsoft.EntityFrameworkCore;

namespace ICPCLike.Services;

public class IcpcService(IcpcLikeContext context)
{
	// 1. Простий запит на вибірку
	public async Task<List<Person>> GetAllParticipants()
	{
		return await context.Persons.ToListAsync();
	}

	// 2. BETWEEN
	public async Task<List<Person>> GetParticipantsBornBetween(DateTime from, DateTime to)
	{
		return await context.Persons
			.Where(p => p.DateOfBirth >= from && p.DateOfBirth <= to)
			.ToListAsync();
	}

	// 3. IN
	public async Task<List<Person>> GetParticipantsByCountries(List<string> countries)
	{
		var orgIds = await context.Organizations
			.Where(o => countries.Contains(o.Country))
			.Select(o => o.Id)
			.ToListAsync();
		
		var teamIds = await context.Teams
			.Where(t => orgIds.Contains(t.OrganizationId ?? 0))
			.Select(t => t.Id)
			.ToListAsync();
		
		return await context.TeamMembers
			.Where(tm => teamIds.Contains(tm.TeamId))
			.Select(tm => tm.Contestant)
			.Distinct()
			.ToListAsync();
	}

	// 4. LIKE
	public async Task<List<Person>> SearchParticipantsByName(string pattern)
	{
		return await context.Persons
			.Where(p => EF.Functions.ILike(p.Name, $"%{pattern}%"))
			.ToListAsync();
	}

	// 5. AND
	public async Task<List<Person>> GetFemaleCoaches()
	{
		return await context.Persons.Where(p => p.Sex == "F" && p.Role == Role.Coach).ToListAsync();
	}

	// 6. OR
	public async Task<List<Person>> GetMaleOrReserve()
	{
		return await context.Persons.Where(p => p.Sex == "M" || p.Role == Role.Reserve).ToListAsync();
	}

	// 7. DISTINCT
	public async Task<List<Role>> GetDistinctRoles()
	{
		return await context.Persons.Select(p => p.Role).Distinct().ToListAsync();
	}
	
	public async Task<List<string>> GetDistinctCountries()
	{
		return await context.Organizations
			.Where(o => o.Country != null)
			.Select(o => o.Country!)
			.Distinct()
			.OrderBy(c => c)
			.ToListAsync();
	}
	
	// 8. MIN
	public async Task<Person?> GetOldestParticipant()
	{
		var minDob = await context.Persons.MinAsync(p => p.DateOfBirth);
		
		return await context.Persons
			.FirstOrDefaultAsync(p => p.DateOfBirth == minDob);
	}

	// 9. AVG
	public async Task<double> GetAverageSolvedTasks()
	{
		return await context.Results.AverageAsync(r => r.SolvedTasks);
	}

	// 10. COUNT
	public async Task<int> GetStageCount()
	{
		return await context.Stages.CountAsync();
	}

	// 11. Агрегатна + додаткові поля
	public async Task<List<(string SeasonName, int TotalSolved)>> GetAvgSolvedTasksPerSeason()
	{
		return await context.Results
			.Include(r => r.Stage).ThenInclude(s => s.Season)
			.GroupBy(r => r.Stage.Season.Name)
			.Select(g => new ValueTuple<string, int>(g.Key, g.Sum(r => r.SolvedTasks)))
			.ToListAsync();
	}

	// 12. Агрегатна + умова на поле
	public async Task<List<(string TeamName, int ResultsCount)>> GetVisibleTeamStats()
	{
		return await context.Results
			.Where(r => !r.Team.Hidden)
			.GroupBy(r => r.Team.Name)
			.Select(g => new ValueTuple<string, int>(g.Key, g.Count()))
			.ToListAsync();
	}

	// 13. Агрегатна + умова на агрегатну
	public async Task<List<Person>> GetParticipantsWithMinRank()
	{
		var min = await context.Results.MinAsync(r => r.Rank);
		
		return await context.Results
			.Where(r => r.Rank == min)
			.SelectMany(r => r.Team.Members.Select(tm => tm.Contestant))
			.Distinct()
			.ToListAsync();
	}

	// 14. Агрегатна + фільтр + сортування
	public async Task<List<NameCountDto>> GetJoinedResults()
	{
		return await context.TeamMembers
			.Include(tm => tm.Team)
			.ThenInclude(t => t.Results)
			.ThenInclude(r => r.Stage)
			.Where(tm => tm.ContestantId != null)
			.SelectMany(tm => tm.Team.Results
				.Where(r =>
					r.Stage.Date >= tm.JoinDate &&
					(tm.LeaveDate == null || r.Stage.Date <= tm.LeaveDate)
				)
				.Select(r => new { tm.Contestant.Name })
			)
			.GroupBy(x => x.Name)
			.Select(g => new NameCountDto
			{
				Name = g.Key,
				Count = g.Count()
			})
			.OrderByDescending(x => x.Count)
			.ToListAsync();
	}

	// 15. INNER JOIN
	public async Task<List<(string Participant, string Team, string Stage, int Rank)>> GetParticipantResults()
	{
		return await context.Results
			.Include(r => r.Team)
			.ThenInclude(t => t.Members)
			.Include(r => r.Stage)
			.SelectMany(r => r.Team.Members
				.Select(m => new ValueTuple<string, string, string, int>(m.Contestant.Name, r.Team.Name, r.Stage.Name, r.Rank)))
			.ToListAsync();
	}

	// 16. LEFT JOIN
	public async Task<List<(Person, Team?)>> GetLeftJoinedParticipants()
	{
		return await (from p in context.Persons
				join tm in context.TeamMembers on p.Id equals tm.ContestantId into gj
				from sub in gj.DefaultIfEmpty()
				join t in context.Teams on sub.TeamId equals t.Id into tj
				from team in tj.DefaultIfEmpty()
				select new ValueTuple<Person, Team?>(p, team))
			.ToListAsync();
	}

	// 17. RIGHT JOIN
	public async Task<List<Person>> GetParticipantsWithRightJoin()
	{
		return await (from p in context.Persons
				join tm in context.TeamMembers on p.Id equals tm.ContestantId into gj
				from sub in gj.DefaultIfEmpty()
				where sub == null
				select p)
			.ToListAsync();
	}

	// 18. INNER JOIN + WHERE
	public async Task<List<Person>> GetParticipantsInStage()
	{
		var stage = await context.Stages.FirstOrDefaultAsync();
		
		return await context.Results
			.Where(r => r.StageId == stage.Id)
			.SelectMany(r => r.Team.Members.Select(tm => tm.Contestant))
			.Distinct()
			.ToListAsync();
	}

	// 19. INNER JOIN + LIKE
	public async Task<List<(Person Participant, string Team)>> SearchParticipantsInTeams(string pattern)
	{
		return await context.TeamMembers
			.Where(tm => EF.Functions.ILike(tm.Contestant.Name, $"%{pattern}%"))
			.Select(tm => new ValueTuple<Person, string>(tm.Contestant, tm.Team.Name))
			.ToListAsync();
	}

	// 20. INNER JOIN + агрегатна
	public async Task<List<(string TeamName, double AvgSolved)>> GetTeamAverageSolved()
	{
		return await context.Results.GroupBy(r => r.Team.Name)
			.Select(g => new ValueTuple<string, double>(g.Key, g.Average(r => r.SolvedTasks)))
			.ToListAsync();
	}

	// 21. INNER JOIN + агрегатна + HAVING
	public async Task<List<(string TeamName, int TotalPenalty)>> GetTeamsWithHighPenalty()
	{
		return await context.Results.GroupBy(r => r.Team.Name)
			.Where(g => g.Sum(r => r.PenaltyTime) > 1000)
			.Select(g => new ValueTuple<string, int>(g.Key, g.Sum(r => r.PenaltyTime)))
			.ToListAsync();
	}

	// 22. Підзапит з = < >
	public async Task<List<Person>> GetParticipantsWithMostResults()
	{
		var maxCount = await context.TeamMembers
			.GroupBy(tm => tm.ContestantId)
			.Select(g => g.Count())
			.MaxAsync();

		return await context.TeamMembers
			.GroupBy(tm => tm.Contestant)
			.Where(g => g.Count() == maxCount)
			.Select(g => g.Key)
			.ToListAsync();
	}

	// 23. Підзапит + агрегатна функція
	public async Task<List<Person>> GetParticipantsWithAboveAvgResults()
	{
		var avgCount = await context.TeamMembers
			.GroupBy(tm => tm.ContestantId)
			.Select(g => g.Count())
			.AverageAsync();

		return await context.TeamMembers
			.GroupBy(tm => tm.Contestant)
			.Where(g => g.Count() > avgCount)
			.Select(g => g.Key)
			.ToListAsync();
	}
	
	// 24. EXISTS
	public async Task<List<Person>> GetParticipantsWithResultsExist()
	{
		return await context.Persons
			.Where(p => context.TeamMembers.Any(tm => tm.ContestantId == p.Id))
            .ToListAsync();
	}

	// 25. ANY / SOME
	public async Task<List<Person>> GetParticipantsWithAnyParticipation()
	{
		var teamIdsWithResults = await context.Results.Select(r => r.TeamId).Distinct().ToListAsync();
		
		return await context.TeamMembers
			.Where(tm => teamIdsWithResults.Any(id => id == tm.TeamId))
			.Select(tm => tm.Contestant)
			.Distinct()
			.ToListAsync();
	}

	// 26. IN + підзапит
	public async Task<List<Person>> GetParticipantsInOrganizations()
	{
		return await context.TeamMembers
			.Where(tm => context.Organizations
				.Where(o => o.Country == "Ukraine")
				.Select(o => o.Id)
				.Contains(tm.Team.OrganizationId ?? 0))
			.Select(tm => tm.Contestant)
			.Distinct()
			.ToListAsync();
	}

	// 27. Підзапит + INNER JOIN
	public async Task<List<(string TeamName, string OrgName)>> GetTeamWithOrgFromSubquery()
	{
		return await context.Teams
			.Where(t => context.Organizations
				.Where(o => o.Country == "Poland")
				.Select(o => o.Id)
				.Contains(t.OrganizationId ?? 0))
			.Join(context.Organizations,
				t => t.OrganizationId,
				o => o.Id,
				(t, o) => new ValueTuple<string, string>(t.Name, o.Name))
			.ToListAsync();
	}
	
	public async Task<List<(string TeamName, string SeasonName, int SubstitutionCount)>> GetTeamsWithSubstitutionsPerSeason()
	{
		var result = await context.Substitutions
			.Join(context.Seasons,
				s => true,
				season => true,
				(s, season) => new
				{
					s.Team.Name,
					SeasonName = season.Name,
					s.TeamId,
					SeasonId = season.Id,
					season.StartDate,
					season.EndDate,
					s.SubstitutionDate
				}
			)
			.Where(x => x.SubstitutionDate >= x.StartDate && x.SubstitutionDate <= x.EndDate)
			.GroupBy(x => new { x.TeamId, x.SeasonId, x.Name, x.SeasonName })
			.Where(g => g.Any())
			.Select(g => new ValueTuple<string, string, int>(
				g.Key.Name,
				g.Key.SeasonName,
				g.Count()
			))
			.ToListAsync();

		return result;
	}
}