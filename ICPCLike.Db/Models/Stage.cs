using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

[Table("stages")]
public class Stage
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("name")]
	public string Name { get; set; }

	[Column("date")]
	public DateTime Date { get; set; }
	
	[Column("level")]
	public Level Level { get; set; }

	[Column("season_id")]
	public int SeasonId { get; set; }

	[ForeignKey("seasonId")]
	public Season Season { get; set; }

	public ICollection<Result> Results { get; set; }
}