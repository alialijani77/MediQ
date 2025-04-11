using MediQ.Core.DTOs.Account.Role;

namespace MediQ.CoreBusiness.Services.Interfaces
{
	public interface IRoleService
	{
		Task<IList<RoleListDto>> GetAllRoles();
		Task<bool> CreateRole(AddNewRoleDto newRoleDto);
	}
}
