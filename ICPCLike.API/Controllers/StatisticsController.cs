using ICPCLike.Db.Models;
using ICPCLike.DTO;
using ICPCLike.Services;
using Microsoft.AspNetCore.Mvc;

namespace ICPCLike.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IcpcService service) : ControllerBase
{
	[HttpGet("participants")]
	public async Task<ActionResult<List<PersonDto>>> GetAllParticipants()
	{
		var people = await service.GetAllParticipants();
		
		return Ok(people.Select(ToDto));
	}

	[HttpGet("participants/between")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsBornBetween([FromQuery] DateTime from, [FromQuery] DateTime to)
	{
		from = DateTime.SpecifyKind(from, DateTimeKind.Utc);
		to = DateTime.SpecifyKind(to, DateTimeKind.Utc);
		
		var people = await service.GetParticipantsBornBetween(from, to);
		
		return Ok(people.Select(ToDto));
	}

	[HttpPost("participants/by-countries")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsByCountries([FromBody] CountryRequest countries)
	{
		var people = await service.GetParticipantsByCountries(countries.Countries);
		
		return Ok(people.Select(ToDto));
	}

	[HttpGet("participants/search")]
	public async Task<ActionResult<List<PersonDto>>> SearchParticipantsByName([FromQuery] string pattern)
	{
		var people = await service.SearchParticipantsByName(pattern);
		
		return Ok(people.Select(ToDto));
	}

	[HttpGet("participants/female-coaches")]
	public async Task<ActionResult<List<PersonDto>>> GetFemaleCoaches()
	{
		var people = await service.GetFemaleCoaches();
		
		return Ok(people.Select(ToDto));
	}

	[HttpGet("participants/male-or-reserve")]
	public async Task<ActionResult<List<PersonDto>>> GetMaleOrReserve()
	{
		var people = await service.GetMaleOrReserve();
		
		return Ok(people.Select(ToDto));
	}

	[HttpGet("roles/distinct")]
	public async Task<ActionResult<List<Role>>> GetDistinctRoles()
	{
		var roles = await service.GetDistinctRoles();
		
		return Ok(roles);
	}

	[HttpGet("participants/oldest")]
	public async Task<ActionResult<PersonDto?>> GetOldestParticipant()
	{
		var person = await service.GetOldestParticipant();
		if (person == null) return NotFound();
		
		return Ok(ToDto(person));
	}

	[HttpGet("results/average-solved-tasks")]
	public async Task<ActionResult<double>> GetAverageSolvedTasks()
	{
		var avg = await service.GetAverageSolvedTasks();
		
		return Ok(avg);
	}
	
	[HttpGet("stages/count")]
	public async Task<ActionResult<int>> GetStageCount()
	{
		var count = await service.GetStageCount();
		
		return Ok(count);
	}

	[HttpGet("results/solved-per-season")]
	public async Task<ActionResult<List<SeasonSolvedDto>>> GetAvgSolvedTasksPerSeason()
	{
		var data = await service.GetAvgSolvedTasksPerSeason();
		
		return Ok(data.Select(d => new SeasonSolvedDto
		{
			SeasonName = d.SeasonName,
			TotalSolved = d.TotalSolved
		}));
	}

	[HttpGet("teams/visible-results")]
	public async Task<ActionResult<List<TeamResultsCountDto>>> GetVisibleTeamStats()
	{
		var data = await service.GetVisibleTeamStats();
		
		return Ok(data.Select(d => new TeamResultsCountDto
		{
			TeamName = d.TeamName,
			ResultsCount = d.ResultsCount
		}));
	}

	[HttpGet("participants/min-rank")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithMinRank()
	{
		var people = await service.GetParticipantsWithMinRank();
		
		return Ok(people.Select(ToDto));
	}

	[HttpGet("participants/joined-count")]
	public async Task<ActionResult<List<NameCountDto>>> GetJoinedResults()
	{
		var data = await service.GetJoinedResults();
		
		return Ok(data.Select(d => new NameCountDto
		{
			Name = d.Name,
			Count = d.Count
		}));
	}

	[HttpGet("participants/results")]
	public async Task<ActionResult<List<ParticipantResultDto>>> GetParticipantResults()
	{
		var results = await service.GetParticipantResults();
		
		return Ok(results.Select(r => new ParticipantResultDto
		{
			Participant = r.Participant,
			Team = r.Team,
			Stage = r.Stage,
			Rank = r.Rank
		}));
	}

	[HttpGet("participants/left-joined")]
	public async Task<ActionResult<List<ParticipantWithTeamDto>>> GetLeftJoinedParticipants()
	{
		var results = await service.GetLeftJoinedParticipants();
		
		return Ok(results.Select(r => new ParticipantWithTeamDto
		{
			Person = ToDto(r.Item1),
			TeamName = r.Item2?.Name
		}));
	}

	[HttpGet("participants/right-joined")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithRightJoin()
	{
		var people = await service.GetParticipantsWithRightJoin();
		
		return Ok(people.Select(ToDto));
	}

	[HttpGet("participants/in-stage")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsInStage()
	{
		var people = await service.GetParticipantsInStage();
		
		return Ok(people.Select(ToDto));
	}
	
	[HttpGet("participants-in-teams")]
	public async Task<ActionResult<List<ParticipantWithTeamDto>>> GetParticipantsInTeams(string pattern)
	{
		var data = await service.SearchParticipantsInTeams(pattern);
		
		return Ok(data.Select(d => new ParticipantWithTeamDto
		{
			Person = new PersonDto(d.Participant),
			TeamName = d.Team
		}));
	}

	[HttpGet("teams/average-solved")]
	public async Task<ActionResult<List<TeamAverageSolvedDto>>> GetTeamAverageSolved()
	{
		var data = await service.GetTeamAverageSolved();

		return Ok(data.Select(d => new TeamAverageSolvedDto
		{
			TeamName = d.TeamName,
			AvgSolved = d.AvgSolved
		}));
	}

	[HttpGet("teams/high-penalty")]
	public async Task<ActionResult<List<TeamHighPenaltyDto>>> GetTeamsWithHighPenalty()
	{
		var data = await service.GetTeamsWithHighPenalty();

		return Ok(data.Select(d => new TeamHighPenaltyDto
		{
			TeamName = d.TeamName,
			TotalPenalty = d.TotalPenalty
		}));
	}

	[HttpGet("participants-most-results")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithMostResults()
	{
		var result = await service.GetParticipantsWithMostResults();
		
		return Ok(result.Select(p => new PersonDto(p)));
	}

	[HttpGet("participants-above-avg")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithAboveAvgResults()
	{
		var result = await service.GetParticipantsWithAboveAvgResults();
		
		return Ok(result.Select(p => new PersonDto(p)));
	}

	[HttpGet("participants-exist")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithResultsExist()
	{
		var result = await service.GetParticipantsWithResultsExist();
		
		return Ok(result.Select(p => new PersonDto(p)));
	}

	[HttpGet("participants-any")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithAnyParticipation()
	{
		var result = await service.GetParticipantsWithAnyParticipation();
		
		return Ok(result.Select(p => new PersonDto(p)));
	}

	[HttpGet("participants-orgs")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsInOrganizations()
	{
		var result = await service.GetParticipantsInOrganizations();
		
		return Ok(result.Select(p => new PersonDto(p)));
	}

	[HttpGet("teams-orgs")]
	public async Task<ActionResult<List<TeamWithOrgDto>>> GetTeamWithOrgFromSubquery()
	{
		var result = await service.GetTeamWithOrgFromSubquery();

		var dto = result.Select(r => new TeamWithOrgDto
		{
			TeamName = r.TeamName,
			OrgName = r.OrgName
		}).ToList();

		return Ok(dto);
	}
	
	[HttpGet("organizations/countries")]
	public async Task<ActionResult<List<string>>> GetDistinctCountries()
	{
		var countries = await service.GetDistinctCountries();
		
		return Ok(countries);
	}
	
	private static PersonDto ToDto(Person p) => new(p);
}