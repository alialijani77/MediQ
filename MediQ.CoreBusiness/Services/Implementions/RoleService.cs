using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.Domain.Interfaces;

namespace MediQ.CoreBusiness.Services.Implementions
{
	public class RoleService : IRoleService
	{
		private readonly IRoleRepository _roleRepository;

		#region ctor
		public RoleService(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}
		#endregion
		#region Role

		#endregion
	}
}
