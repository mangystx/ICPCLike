using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

/// \ingroup Models
/// \brief Заміна учасника в команді.
/// \details Фіксує дату, попереднього та нового учасника.
[Table("substitutions")]
public class Substitution
{
	/// \brief Первинний ключ.
	[Key]
	[Column("id")]
	public int Id { get; set; }

	/// \brief Зовнішній ключ на команду.
	[Column("team_id")]
	public int TeamId { get; set; }

	/// \brief Команда, де відбулася заміна.
	[ForeignKey(nameof(TeamId))]
	public Team Team { get; set; }

	/// \brief Ідентифікатор учасника, якого замінили.
	[Column("old_contestant_id")]
	public int OldContestantId { get; set; }

	/// \brief Старий учасник (той, кого замінили).
	[ForeignKey(nameof(OldContestantId))]
	public Person OldContestant { get; set; }

	/// \brief Ідентифікатор нового учасника.
	[Column("new_contestant_id")]
	public int NewContestantId { get; set; }

	/// \brief Новий учасник (той, хто прийшов на заміну).
	[ForeignKey(nameof(NewContestantId))]
	public Person NewContestant { get; set; }

	/// \brief Дата проведення заміни.
	[Column("substitution_date")]
	public DateTime SubstitutionDate { get; set; }
}