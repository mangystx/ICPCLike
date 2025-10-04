using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

/// \ingroup Models
/// \brief Персона/учасник/тренер.
/// \details Містить базові дані, роль у команді та дати народження для статистики.
[Table("persons")]
public class Person
{
	/// \brief Первинний ключ.
	[Key]
	[Column("id")]
	public int Id { get; set; }

	/// \brief Зовнішній ICPC-ідентифікатор (опційно).
	[Column("icpc_id")]
	public string? IcpcId { get; set; }

	/// \brief ПІБ або загальна назва профілю.
	[Column("name")]
	public string Name { get; set; }

	/// \brief Академічне/професійне звання (опційно).
	[Column("title")]
	public string? Title { get; set; }

	/// \brief E-mail (опційно).
	[Column("email")]
	public string? Email { get; set; }

	/// \brief Стать ('M'/'F'/інше) — вільний формат (опційно).
	[Column("sex")]
	public string? Sex { get; set; }

	/// \brief Роль у команді/системі (учасник/резерв/тренер).
	[Column("role")]
	public Role Role { get; set; }

	/// \brief Дата народження (UTC, для обчислення віку/фільтрів).
	[Column("date_of_birth")]
	public DateTime DateOfBirth { get; set; }

	/// \brief Членства в командах (історія складів).
	/// \note Навігаційна колекція EF Core.
	public ICollection<TeamMember> TeamMemberships { get; set; }
}