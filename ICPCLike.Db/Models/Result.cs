using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

/// \ingroup Models
/// \brief Підсумок виступу команди на конкретному етапі.
/// \details Містить місце, кількість розв'язаних задач і штрафний час.
[Table("results")]
public class Result
{
	/// \brief Первинний ключ.
	[Key]
	[Column("id")]
	public int Id { get; set; }

	/// \brief Зовнішній ключ на етап.
	[Column("stage_id")]
	public int StageId { get; set; }

	/// \brief Етап, до якого відноситься результат.
	[ForeignKey(nameof(StageId))]
	public Stage Stage { get; set; }

	/// \brief Зовнішній ключ на команду.
	[Column("team_id")]
	public int TeamId { get; set; }

	/// \brief Команда, що отримала результат.
	[ForeignKey(nameof(TeamId))]
	public Team Team { get; set; }

	/// \brief Ранг/місце команди.
	[Column("rank")]
	public int Rank { get; set; }

	/// \brief Кількість розв’язаних задач.
	[Column("solved_tasks")]
	public int SolvedTasks { get; set; }

	/// \brief Сумарний штрафний час (хв/сек — залежно від домовленості).
	[Column("penalty_time")]
	public int PenaltyTime { get; set; }
}