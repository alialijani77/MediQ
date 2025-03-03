using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MediQ.Infra.Data.DataContext
{
	public class SqlServerDesignTimeFactory : IDesignTimeDbContextFactory<SqlServerContext>
	{
		public SqlServerContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false, false)
				.Build();

			return new SqlServerContext(configuration.GetConnectionString("SqlServerContext"), null);
		}
	}
}