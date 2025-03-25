using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.CoreBusiness.Services.Implementions;
using Microsoft.Extensions.DependencyInjection;
using MediQ.Domain.Interfaces;
using MediQ.Infra.Data.Repositories;
using MediQ.Infra.Data.DataContext;

namespace MediQ.Infra.Ioc
{
	public static class DependencyContainer
	{
		public static void RegisterDependencies(IServiceCollection services,string connectionString)
		{
			#region Context
			services.AddScoped(_ => { return BaseContext.CreateInstance(connectionString, null); });
			#endregion

			#region Services
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IEmailService, EmailService>();
			#endregion

			#region Repositories
			services.AddScoped<IUserRepository, UserRepository>();
			#endregion
		}
	}
}
