using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.CoreBusiness.Services.Implementions;
using Microsoft.Extensions.DependencyInjection;
using MediQ.Domain.Interfaces;
using MediQ.Infra.Data.Repositories;

namespace MediQ.Infra.Ioc
{
	public static class DependencyContainer
	{
		public static void RegisterDependencies(IServiceCollection services)
		{
			#region Services
			services.AddScoped<IUserService, UserService>();

			#endregion

			#region Repositories
			services.AddScoped<IUserRepository, UserRepository>();
			#endregion
		}
	}
}
