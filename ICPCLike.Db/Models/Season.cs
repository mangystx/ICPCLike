using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

[Table("seasons")]
public class Season
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime EndDate { get; set; }

	[Column("name")]
	public string Name { get; set; }

	public ICollection<Stage> Stages { get; set; }
}