using ICPCLike.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace ICPCLike.Db.Context;

public class IcpcLikeContext : DbContext
{
    public IcpcLikeContext(DbContextOptions<IcpcLikeContext> options) : base(options) { }
    
    public DbSet<Person> Persons { get; set; }
	public DbSet<Organization> Organizations { get; set; }
	public DbSet<Season> Seasons { get; set; }
	public DbSet<Stage> Stages { get; set; }
	public DbSet<Team> Teams { get; set; }
	public DbSet<TeamMember> TeamMembers { get; set; }
	public DbSet<Result> Results { get; set; }
	public DbSet<Substitution> Substitutions { get; set; }
}