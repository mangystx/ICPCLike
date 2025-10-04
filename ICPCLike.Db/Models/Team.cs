using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

/// \ingroup Models
/// \brief Команда.
/// \details Має назву, належить до організації (опційно), може бути прихованою та має набір учасників і результати.
[Table("teams")]
public class Team
{
	/// \brief Первинний ключ.
	[Key]
	[Column("id")]
	public int Id { get; set; }

	/// \brief Назва команди.
	[Column("name")]
	public string Name { get; set; }

	/// \brief Зовнішній ключ на організацію (опційно).
	[Column("organization_id")]
	public int? OrganizationId { get; set; }

	/// \brief Організація, до якої належить команда.
	[ForeignKey(nameof(OrganizationId))]
	public Organization? Organization { get; set; }

	/// \brief Ознака прихованості команди.
	[Column("hidden")]
	public bool Hidden { get; set; }

	/// \brief Склад команди (учасники з датами приєднання).
	public ICollection<TeamMember> Members { get; set; }

	/// \brief Результати команди на різних етапах.
	public ICollection<Result> Results { get; set; }
}