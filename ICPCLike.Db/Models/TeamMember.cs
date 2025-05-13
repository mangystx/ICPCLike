using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

[Table("team_members")]
public class TeamMember
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("team_id")]
	public int TeamId { get; set; }

	[ForeignKey("teamId")]
	public Team Team { get; set; }

	[Column("contestant_id")]
	public int ContestantId { get; set; }

	[ForeignKey("contestantId")]
	public Person Contestant { get; set; }

	[Column("join_date")]
	public DateTime JoinDate { get; set; }

	[Column("leave_date")]
	public DateTime? LeaveDate { get; set; }
}