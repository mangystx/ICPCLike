using ICPCLike.Db.Models;
using ICPCLike.Services;
using ICPCLIke.Services;
using Microsoft.AspNetCore.Mvc;

namespace ICPCLike.Controllers;

/// \ingroup Controllers
/// \brief Контролер статистики для ICPC-подібної системи.
/// \details Надає ендпоїнти вибірок, агрегацій і звітності.
[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IcpcService service) : ControllerBase
{
	/// \brief Отримати всіх учасників.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants
	[HttpGet("participants")]
	public async Task<ActionResult<List<PersonDto>>> GetAllParticipants()
	{
		var people = await service.GetAllParticipants();
		return Ok(people.Select(ToDto));
	}

	/// \brief Отримати учасників, народжених у вказаному діапазоні дат (UTC).
	/// \param from Початок інтервалу (включно), UTC.
	/// \param to Кінець інтервалу (включно), UTC.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants/between?from=...&to=...
	[HttpGet("participants/between")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsBornBetween([FromQuery] DateTime from, [FromQuery] DateTime to)
	{
		from = DateTime.SpecifyKind(from, DateTimeKind.Utc);
		to = DateTime.SpecifyKind(to, DateTimeKind.Utc);

		var people = await service.GetParticipantsBornBetween(from, to);
		return Ok(people.Select(ToDto));
	}

	/// \brief Отримати учасників за переліком країн.
	/// \param countries Тіло запиту з переліком країн.
	/// \return Колекція \ref PersonDto.
	/// \note Route: POST /api/Statistics/participants/by-countries
	[HttpPost("participants/by-countries")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsByCountries([FromBody] CountryRequest countries)
	{
		var people = await service.GetParticipantsByCountries(countries.Countries);
		return Ok(people.Select(ToDto));
	}

	/// \brief Пошук учасників за шаблоном імені (LIKE/ILIKE).
	/// \param pattern Рядок пошуку.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants/search?pattern=...
	[HttpGet("participants/search")]
	public async Task<ActionResult<List<PersonDto>>> SearchParticipantsByName([FromQuery] string pattern)
	{
		var people = await service.SearchParticipantsByName(pattern);
		return Ok(people.Select(ToDto));
	}

	/// \brief Отримати тренерок (Female) з роллю Coach.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants/female-coaches
	[HttpGet("participants/female-coaches")]
	public async Task<ActionResult<List<PersonDto>>> GetFemaleCoaches()
	{
		var people = await service.GetFemaleCoaches();
		return Ok(people.Select(ToDto));
	}

	/// \brief Отримати учасників чоловічої статі або зі статусом «reserve».
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants/male-or-reserve
	[HttpGet("participants/male-or-reserve")]
	public async Task<ActionResult<List<PersonDto>>> GetMaleOrReserve()
	{
		var people = await service.GetMaleOrReserve();
		return Ok(people.Select(ToDto));
	}

	/// \brief Отримати унікальні ролі учасників.
	/// \return Перелік \ref Role.
	/// \note Route: GET /api/Statistics/roles/distinct
	[HttpGet("roles/distinct")]
	public async Task<ActionResult<List<Role>>> GetDistinctRoles()
	{
		var roles = await service.GetDistinctRoles();
		return Ok(roles);
	}

	/// \brief Отримати найстаршого учасника.
	/// \return \ref PersonDto або 404 якщо не знайдено.
	/// \note Route: GET /api/Statistics/participants/oldest
	[HttpGet("participants/oldest")]
	public async Task<ActionResult<PersonDto?>> GetOldestParticipant()
	{
		var person = await service.GetOldestParticipant();
		if (person == null) return NotFound();
		return Ok(ToDto(person));
	}

	/// \brief Середня кількість розв’язаних задач усіма учасниками.
	/// \return Значення середнього.
	/// \note Route: GET /api/Statistics/results/average-solved-tasks
	[HttpGet("results/average-solved-tasks")]
	public async Task<ActionResult<double>> GetAverageSolvedTasks()
	{
		var avg = await service.GetAverageSolvedTasks();
		return Ok(avg);
	}

	/// \brief Кількість етапів (stages).
	/// \return Ціле число.
	/// \note Route: GET /api/Statistics/stages/count
	[HttpGet("stages/count")]
	public async Task<ActionResult<int>> GetStageCount()
	{
		var count = await service.GetStageCount();
		return Ok(count);
	}

	/// \brief Сумарно розв’язані задачі по сезонах.
	/// \return Колекція \ref SeasonSolvedDto.
	/// \note Route: GET /api/Statistics/results/solved-per-season
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

	/// \brief Статистика видимих (опублікованих) результатів по командах.
	/// \return Колекція \ref TeamResultsCountDto.
	/// \note Route: GET /api/Statistics/teams/visible-results
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

	/// \brief Учасники з мінімальним рангом.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants/min-rank
	[HttpGet("participants/min-rank")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithMinRank()
	{
		var people = await service.GetParticipantsWithMinRank();
		return Ok(people.Select(ToDto));
	}

	/// \brief Об’єднані результати (ім’я + кількість входжень).
	/// \return Колекція \ref NameCountDto.
	/// \note Route: GET /api/Statistics/participants/joined-count
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

	/// \brief Результати участі з приєднаннями учасник–команда–етап.
	/// \return Колекція \ref ParticipantResultDto.
	/// \note Route: GET /api/Statistics/participants/results
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

	/// \brief Left-join учасників до команд (включно з тими, хто без команди).
	/// \return Колекція \ref ParticipantWithTeamDto.
	/// \note Route: GET /api/Statistics/participants/left-joined
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

	/// \brief Right-join учасників до команд (учасники, які мають відповідність у командах).
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants/right-joined
	[HttpGet("participants/right-joined")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithRightJoin()
	{
		var people = await service.GetParticipantsWithRightJoin();
		return Ok(people.Select(ToDto));
	}

	/// \brief Учасники, що брали участь у принаймні одному етапі.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants/in-stage
	[HttpGet("participants/in-stage")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsInStage()
	{
		var people = await service.GetParticipantsInStage();
		return Ok(people.Select(ToDto));
	}

	/// \brief Пошук учасників у командах за шаблоном.
	/// \param pattern Шаблон для пошуку по ПІБ або назві команди.
	/// \return Колекція \ref ParticipantWithTeamDto.
	/// \note Route: GET /api/Statistics/participants-in-teams?pattern=...
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

	/// \brief Середня кількість розв’язаних задач по командах.
	/// \return Колекція \ref TeamAverageSolvedDto.
	/// \note Route: GET /api/Statistics/teams/average-solved
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

	/// \brief Команди з високою сумою штрафів.
	/// \return Колекція \ref TeamHighPenaltyDto.
	/// \note Route: GET /api/Statistics/teams/high-penalty
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

	/// \brief Учасники з найбільшою кількістю результатів.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants-most-results
	[HttpGet("participants-most-results")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithMostResults()
	{
		var result = await service.GetParticipantsWithMostResults();
		return Ok(result.Select(p => new PersonDto(p)));
	}

	/// \brief Учасники з результатами вище середнього.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants-above-avg
	[HttpGet("participants-above-avg")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithAboveAvgResults()
	{
		var result = await service.GetParticipantsWithAboveAvgResults();
		return Ok(result.Select(p => new PersonDto(p)));
	}

	/// \brief Учасники, для яких існують результати.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants-exist
	[HttpGet("participants-exist")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithResultsExist()
	{
		var result = await service.GetParticipantsWithResultsExist();
		return Ok(result.Select(p => new PersonDto(p)));
	}

	/// \brief Учасники з будь-якою участю (ANY).
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants-any
	[HttpGet("participants-any")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsWithAnyParticipation()
	{
		var result = await service.GetParticipantsWithAnyParticipation();
		return Ok(result.Select(p => new PersonDto(p)));
	}

	/// \brief Учасники, що входять до організацій.
	/// \return Колекція \ref PersonDto.
	/// \note Route: GET /api/Statistics/participants-orgs
	[HttpGet("participants-orgs")]
	public async Task<ActionResult<List<PersonDto>>> GetParticipantsInOrganizations()
	{
		var result = await service.GetParticipantsInOrganizations();
		return Ok(result.Select(p => new PersonDto(p)));
	}

	/// \brief Команди разом із організаціями (через підзапит).
	/// \return Колекція \ref TeamWithOrgDto.
	/// \note Route: GET /api/Statistics/teams-orgs
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

	/// \brief Унікальні країни організацій.
	/// \return Перелік рядків (назви країн).
	/// \note Route: GET /api/Statistics/organizations/countries
	[HttpGet("organizations/countries")]
	public async Task<ActionResult<List<string>>> GetDistinctCountries()
	{
		var countries = await service.GetDistinctCountries();
		return Ok(countries);
	}

	/// \brief Кількість замін у командах по сезонах.
	/// \return Колекція \ref TeamSubstitutionDto.
	/// \note Route: GET /api/Statistics/teams/with-substitutions
	[HttpGet("teams/with-substitutions")]
	public async Task<ActionResult<List<TeamSubstitutionDto>>> GetTeamsWithSubstitutions()
	{
		var tuples = await service.GetTeamsWithSubstitutionsPerSeason();
		var result = tuples
			.Select(t => new TeamSubstitutionDto
			{
				TeamName = t.TeamName,
				SeasonName = t.SeasonName,
				SubstitutionCount = t.SubstitutionCount
			})
			.ToList();

		return Ok(result);
	}

	/// \brief Мапер з \ref Person у \ref PersonDto.
	private static PersonDto ToDto(Person p) => new(p);
}