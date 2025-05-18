using ICPCLike.Db.Models;

namespace ICPCLike.DTO;

public class PersonDto(Person person)
{
	public int Id { get; set; } = person.Id;
	public string Name { get; set; } = person.Name;
	public string? Sex { get; set; } = person.Sex;
	public DateTime DateOfBirth { get; set; } = person.DateOfBirth;
	public string Role { get; set; } = person.Role.ToString();
}

public class SeasonSolvedDto
{
	public string SeasonName { get; set; }
	public int TotalSolved { get; set; }
}

public class TeamResultsCountDto
{
	public string TeamName { get; set; }
	public int ResultsCount { get; set; }
}

public class ParticipantResultDto
{
	public string Participant { get; set; }
	public string Team { get; set; }
	public string Stage { get; set; }
	public int Rank { get; set; }
}

public class ParticipantWithTeamDto
{
	public PersonDto Person { get; set; }
	public string? TeamName { get; set; }
}

public class TeamAverageSolvedDto
{
	public string TeamName { get; set; } = default!;
	public double AvgSolved { get; set; }
}

public class TeamHighPenaltyDto
{
	public string TeamName { get; set; } = default!;
	public int TotalPenalty { get; set; }
}

public class TeamWithOrgDto
{
	public string TeamName { get; set; } = default!;
	public string OrgName { get; set; } = default!;
}

public class CountryRequest
{
	public List<string> Countries { get; set; } = [];
}

public class NameCountDto
{
	public string Name { get; set; } = string.Empty;
	public int Count { get; set; }
}