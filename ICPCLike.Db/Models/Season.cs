using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

/// \ingroup Models
/// \brief Сезон змагань (період із назвою).
[Table("seasons")]
public class Season
{
	/// \brief Первинний ключ.
	[Key]
	[Column("id")]
	public int Id { get; set; }

	/// \brief Дата початку сезону (UTC).
	[Column("start_date")]
	public DateTime StartDate { get; set; }

	/// \brief Дата завершення сезону (UTC).
	[Column("end_date")]
	public DateTime EndDate { get; set; }

	/// \brief Людинозрозуміла назва сезону (наприклад, "2023/24").
	[Column("name")]
	public string Name { get; set; }

	/// \brief Етапи, що входять до сезону.
	public ICollection<Stage> Stages { get; set; }
}