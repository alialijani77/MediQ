using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace MediQ.Infra.Data.DataContext
{
	public class SqlServerContext : BaseContext
	{
		private readonly string _connectionstring;
		private readonly ILoggerFactory _loggerFactory;
		private static int _commandTimeout;

		public SqlServerContext(string connectionstring, ILoggerFactory loggerFactory, int commandTimeout = 60)
		{
			_connectionstring = connectionstring;
			_loggerFactory = loggerFactory;
			_commandTimeout = commandTimeout;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored))
				.UseSqlServer(
					_connectionstring,
					options =>
					{
						options.CommandTimeout(_commandTimeout);
						options.MigrationsHistoryTable("A0_MIGRATIONS_HISTORY", "MEDIQ");
					}
				 );
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

		}
	}
}