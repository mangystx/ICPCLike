using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICPCLike.Db.Models;

[Table("results")]
public class Result
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("stage_id")]
	public int StageId { get; set; }

	[ForeignKey("stageId")]
	public Stage Stage { get; set; }

	[Column("team_id")]
	public int TeamId { get; set; }

	[ForeignKey("teamId")]
	public Team Team { get; set; }

	[Column("rank")]
	public int Rank { get; set; }

	[Column("solved_tasks")]
	public int SolvedTasks { get; set; }

	[Column("penalty_time")]
	public int PenaltyTime { get; set; }
}