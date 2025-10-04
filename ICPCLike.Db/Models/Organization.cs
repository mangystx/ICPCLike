using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

/// \ingroup Models
/// \brief Організація (виш/компанія), до якої належать команди.
/// \details Використовується для групування команд та агрегації статистики за країнами.
[Table("organizations")]
public class Organization
{
	/// \brief Первинний ключ.
	[Key]
	[Column("id")]
	public int Id { get; set; }

	/// \brief Назва організації.
	[Column("name")]
	public string Name { get; set; }

	/// \brief Країна розташування (опційно).
	[Column("country")]
	public string? Country { get; set; }

	/// \brief Команди, що належать цій організації.
	/// \note Навігаційна властивість EF Core.
	public ICollection<Team> Teams { get; set; }
}