using MediQ.Domain.Entities.Common;
using MediQ.Domain.Entities.UserManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediQ.Infra.Data.DataContext
{
	public abstract class BaseContext : IdentityDbContext<User, Role, string>
	{
		protected BaseContext() { }

		public static BaseContext CreateInstance(string connectionString, ILoggerFactory loggerFactory)
		{
			return new SqlServerContext(connectionString, loggerFactory);
		}

		#region DbSet
		public DbSet<EmailSetting> EmailSettings { get; set; }

		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.HasDefaultSchema("MediQ");

			#region SeadData
			var date = DateTime.MinValue;

			builder.Entity<EmailSetting>().HasData(new EmailSetting()
			{
				CreateDate = date,
				DisplayName = "MediQ",
				EnableSSL = true,
				From = "mediqsystem@gmail.com",
				Id = 1,
				IsDefault = true,
				IsDelete = false,
				Password = "tkisthnoqbtvsmlq",
				Port = 587,
				SMTP = "smtp.gmail.com"

			});
			#endregion
		}
	}
}