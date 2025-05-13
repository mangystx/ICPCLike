using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

[Table("substitutions")]
public class Substitution
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("team_id")]
	public int TeamId { get; set; }

	[ForeignKey("teamId")]
	public Team Team { get; set; }

	[Column("old_contestant_id")]
	public int OldContestantId { get; set; }

	[ForeignKey("oldContestantId")]
	public Person OldContestant { get; set; }

	[Column("new_contestant_id")]
	public int NewContestantId { get; set; }

	[ForeignKey("newContestantId")]
	public Person NewContestant { get; set; }

	[Column("substitution_date")]
	public DateTime SubstitutionDate { get; set; }
}