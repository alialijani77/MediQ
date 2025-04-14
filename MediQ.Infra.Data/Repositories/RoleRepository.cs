using MediQ.Domain.Entities.UserManagement;
using MediQ.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MediQ.Infra.Data.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		#region ctor
		private readonly RoleManager<Role> _roleManager;

		public RoleRepository(RoleManager<Role> roleManager)
		{
			_roleManager = roleManager;
		}
		#endregion
		#region Roles
		public async Task<IList<Role>> GetAllRoles()
		{
			var result = _roleManager.Roles.ToList();
			return result;
		}


		public async Task<IdentityResult> CreateRole(Role role)
		{
			var result = await _roleManager.CreateAsync(role);
			return result;
		}

		public async Task<IdentityResult> UpdateRole(Role role)
		{
			var result = await _roleManager.UpdateAsync(role);
			return result;
		}
		#endregion

	}
}