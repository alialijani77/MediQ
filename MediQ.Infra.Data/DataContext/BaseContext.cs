using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediQ.Infra.Data.DataContext
{
	public abstract class BaseContext : IdentityDbContext
	{
		protected BaseContext() { }

		public static BaseContext CreateInstance(string connectionString, ILoggerFactory loggerFactory)
		{
			return new SqlServerContext(connectionString, loggerFactory);
		}
	}
}