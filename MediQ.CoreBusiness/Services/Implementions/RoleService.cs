using MediQ.Core.DTOs.Account.Role;
using MediQ.Core.DTOs.Admin.Users;
using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.Domain.Interfaces;
using MediQ.Infra.Data.Repositories;

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
		public async Task<IList<RoleListDto>> GetAllRoles()
		{
			var roles = await _roleRepository.GetAllRoles();
			return roles.Select(u => new RoleListDto
			{
				Id = u.Id,
				Name = u.Name,
				Description = u.Description,
			}).ToList();
		}
		#endregion
	}
}
