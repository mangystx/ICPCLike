namespace ICPCLike.Db.Models;

/// \ingroup Models
/// \brief Роль особи в контексті виступів/команд.
public enum Role
{
	/// \brief Основний учасник.
	Contestant,

	/// \brief Учасник запасу.
	Reserve,

	/// \brief Тренер/коуч.
	Coach
}