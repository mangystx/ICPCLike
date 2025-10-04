using ICPCLike.Db.Models;

namespace ICPCLIke.Services;

/// \ingroup DTO
/// \brief DTO для передачі інформації про особу.
/// \details Використовується в контролерах для мінімізації залежності від внутрішньої моделі Person.
public class PersonDto(Person person)
{
	/// \brief Ідентифікатор особи.
	public int Id { get; set; } = person.Id;

	/// \brief Ім’я або ПІБ.
	public string Name { get; set; } = person.Name;

	/// \brief Стать (опційно).
	public string? Sex { get; set; } = person.Sex;

	/// \brief Дата народження.
	public DateTime DateOfBirth { get; set; } = person.DateOfBirth;

	/// \brief Роль у вигляді рядка.
	public string Role { get; set; } = person.Role.ToString();
}

/// \ingroup DTO
/// \brief Кількість розв’язаних задач у сезоні.
public class SeasonSolvedDto
{
	public string SeasonName { get; set; }
	public int TotalSolved { get; set; }
}

/// \ingroup DTO
/// \brief Кількість результатів для конкретної команди.
public class TeamResultsCountDto
{
	public string TeamName { get; set; }
	public int ResultsCount { get; set; }
}

/// \ingroup DTO
/// \brief Результат участі конкретного учасника.
public class ParticipantResultDto
{
	public string Participant { get; set; }
	public string Team { get; set; }
	public string Stage { get; set; }
	public int Rank { get; set; }
}

/// \ingroup DTO
/// \brief Зв’язка учасник–команда.
public class ParticipantWithTeamDto
{
	public PersonDto Person { get; set; }
	public string? TeamName { get; set; }
}

/// \ingroup DTO
/// \brief Середня кількість розв’язаних задач командою.
public class TeamAverageSolvedDto
{
	public string TeamName { get; set; } = default!;
	public double AvgSolved { get; set; }
}

/// \ingroup DTO
/// \brief Команди з високим штрафним часом.
public class TeamHighPenaltyDto
{
	public string TeamName { get; set; } = default!;
	public int TotalPenalty { get; set; }
}

/// \ingroup DTO
/// \brief Команда разом із організацією.
public class TeamWithOrgDto
{
	public string TeamName { get; set; } = default!;
	public string OrgName { get; set; } = default!;
}

/// \ingroup DTO
/// \brief Запит для вибірки учасників за списком країн.
public class CountryRequest
{
	public List<string> Countries { get; set; } = [];
}

/// \ingroup DTO
/// \brief Пара ім’я–кількість (агрегації).
public class NameCountDto
{
	public string Name { get; set; } = string.Empty;
	public int Count { get; set; }
}

/// \ingroup DTO
/// \brief Інформація про заміни у командах по сезонах.
public class TeamSubstitutionDto
{
	public string TeamName { get; set; }
	public string SeasonName { get; set; }
	public int SubstitutionCount { get; set; }
}