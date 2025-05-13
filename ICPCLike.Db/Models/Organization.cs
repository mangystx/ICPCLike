using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

[Table("organizations")]
public class Organization
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("name")]
	public string Name { get; set; }

	[Column("country")]
	public string? Country { get; set; }

	public ICollection<Team> Teams { get; set; }
}