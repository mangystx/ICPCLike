using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

[Table("persons")]
public class Person
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("icpc_id")]
	public string? IcpcId { get; set; }

	[Column("name")]
	public string Name { get; set; }

	[Column("title")]
	public string? Title { get; set; }

	[Column("email")]
	public string? Email { get; set; }

	[Column("sex")]
	public string? Sex { get; set; }

	[Column("role")]
	public Role Role { get; set; }
	
	[Column("date_of_birth")]
	public DateTime DateOfBirth { get; set; }
	
	public ICollection<TeamMember> TeamMemberships { get; set; }
}