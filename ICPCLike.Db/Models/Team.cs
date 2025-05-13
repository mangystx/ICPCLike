using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

[Table("teams")]
public class Team
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("name")]
	public string Name { get; set; }
	
	[Column("organization_id")]
	public int? OrganizationId { get; set; }

	[ForeignKey("organizationId")]
	public Organization? Organization { get; set; }

	[Column("hidden")]
	public bool Hidden { get; set; }

	public ICollection<TeamMember> Members { get; set; }
	public ICollection<Result> Results { get; set; }
}