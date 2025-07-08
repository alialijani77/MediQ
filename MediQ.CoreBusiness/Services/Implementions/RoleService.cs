using MediQ.Core.DTOs.Account.Role;
using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.Domain.Entities.UserManagement;
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
        public async Task<IList<RoleListDto>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRoles();
            return roles.Select(u => new RoleListDto
            {
                Id = u.Id,
                Name = u.Name,
            }).ToList();
        }


        public async Task<bool> CreateRole(AddNewRoleDto newRoleDto)
        {
            Role role = new Role();
            role.Name = newRoleDto.Name;
            var result = await _roleRepository.CreateRole(role);
            return result.Succeeded;
        }

        public async Task<bool> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var role = await _roleRepository.FindByIdAsync(updateRoleDto.Id);
            if (role is not null)
            {
                role.Name = updateRoleDto.Name;
                var identityResult = await _roleRepository.UpdateRole(role);
                return identityResult.Succeeded;
            }
            return false;
        }
        #endregion
    }
}
