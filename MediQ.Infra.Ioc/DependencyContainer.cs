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
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IEmailService, EmailService>();
			#endregion

			#region Repositories
			services.AddTransient<IUserRepository, UserRepository>();
			#endregion
		}
	}
}
