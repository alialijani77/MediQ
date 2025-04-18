﻿using MediQ.Domain.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;

namespace MediQ.Domain.Interfaces
{
	public interface IRoleRepository
	{
		Task<IList<Role>> GetAllRoles();
		Task<IdentityResult> CreateRole(Role role);
		Task<IdentityResult> UpdateRole(Role role);
		Task<Role> FindByIdAsync(string roleId);

	}
}