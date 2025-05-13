using ICPCLike.Db;
using ICPCLike.Db.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<IcpcLikeContext>(
	options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<IcpcLikeContext>();
	
	TestData.Seed(context);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();