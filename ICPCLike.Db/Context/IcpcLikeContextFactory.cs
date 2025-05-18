using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ICPCLike.Db.Context;

public class IcpcLikeContextFactory : IDesignTimeDbContextFactory<IcpcLikeContext>
{
	public IcpcLikeContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ICPCLike.API"))
			.AddJsonFile("appsettings.json")
			.Build();

		var optionsBuilder = new DbContextOptionsBuilder<IcpcLikeContext>();
		optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

		return new IcpcLikeContext(optionsBuilder.Options);
	}
}