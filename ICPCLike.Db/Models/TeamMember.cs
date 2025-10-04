using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

/// \ingroup Models
/// \brief Членство учасника в команді.
/// \details Відображає зв'язок учасника з командою у певний період (join/leave date).
[Table("team_members")]
public class TeamMember
{
	/// \brief Первинний ключ.
	[Key]
	[Column("id")]
	public int Id { get; set; }

	/// \brief Зовнішній ключ на команду.
	[Column("team_id")]
	public int TeamId { get; set; }

	/// \brief Команда, до якої належить членство.
	[ForeignKey(nameof(TeamId))]
	public Team Team { get; set; }

	/// \brief Зовнішній ключ на учасника.
	[Column("contestant_id")]
	public int ContestantId { get; set; }

	/// \brief Учасник, що входить до складу команди.
	[ForeignKey(nameof(ContestantId))]
	public Person Contestant { get; set; }

	/// \brief Дата приєднання учасника до команди.
	[Column("join_date")]
	public DateTime JoinDate { get; set; }

	/// \brief Дата виходу з команди (null, якщо досі у складі).
	[Column("leave_date")]
	public DateTime? LeaveDate { get; set; }
}