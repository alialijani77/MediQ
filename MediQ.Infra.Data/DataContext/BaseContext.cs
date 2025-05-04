using MediQ.Domain.Entities.Common;
using MediQ.Domain.Entities.UserManagement;
using MediQ.Infra.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        #region DbSet
        public DbSet<EmailSetting> EmailSettings { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("MediQ");

            #region Configurations
            new EmailSettingConfiguration().Configure(builder.Entity<EmailSetting>());
            new RoleConfiguration().Configure(builder.Entity<Role>());
            new UserConfiguration().Configure(builder.Entity<User>());
            #endregion
        }
    }
}