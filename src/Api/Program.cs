using EFCoreEncapsulation.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFCoreEncapsulation.Api;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services
			.AddScoped(_ => new SchoolContext(builder.Configuration["ConnectionString"], true))
			.AddTransient<StudentRepository>() //creates instance for each request
			.AddTransient<Repository<Course>>() //creates instance for each request
			.AddControllers();
		
		// regular config 
		// builder.Services.AddDbContext<SchoolContext>(options =>
		// options
		// 	.UseSqlServer(builder.Configuration["ConnectionString"])
		// 	.UseLoggerFactory(LoggerFactory.Create(b =>
		// 	.EnableSensitiveDataLogging());
		// builder.Services.AddControllers();

		var app = builder.Build();

		// Configure the HTTP request pipeline

		app.MapControllers();

		app.Run();
	}

	
}