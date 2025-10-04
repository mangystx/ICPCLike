using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

/// \ingroup Models
/// \brief Етап змагань.
/// \details Має назву, дату проведення, рівень (регіональний/національний) та належить до сезону.
[Table("stages")]
public class Stage
{
	/// \brief Первинний ключ.
	[Key]
	[Column("id")]
	public int Id { get; set; }

	/// \brief Назва етапу.
	[Column("name")]
	public string Name { get; set; }

	/// \brief Дата проведення етапу.
	[Column("date")]
	public DateTime Date { get; set; }

	/// \brief Рівень етапу (регіональний/національний).
	[Column("level")]
	public Level Level { get; set; }

	/// \brief Зовнішній ключ на сезон.
	[Column("season_id")]
	public int SeasonId { get; set; }

	/// \brief Сезон, до якого належить етап.
	[ForeignKey(nameof(SeasonId))]
	public Season Season { get; set; }

	/// \brief Результати команд у цьому етапі.
	public ICollection<Result> Results { get; set; }
}