using MediQ.Core.DTOs.Account.User;
using MediQ.Core.DTOs.Admin.Users;
using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.Domain.Entities.UserManagement;
using MediQ.Domain.Interfaces;

namespace MediQ.CoreBusiness.Services.Implementions
{
	public class AdminService : IAdminService
	{
		private readonly IUserRepository _userRepository;

		#region ctor
		public AdminService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		#endregion
		#region User
		public async Task<IList<UserListDto>> GetAllUsers()
		{
			var users = await _userRepository.GetAllUsers();
			return users.Select(u => new UserListDto
			{
				Id = u.Id,
				FirstName = u.FirstName,
				LastName = u.LastName,
				UserName = u.UserName,
				PhoneNumber = u.PhoneNumber,
				EmailConfirmed = u.EmailConfirmed,
				AccessFailedCount = u.AccessFailedCount
			}).ToList();
		}

		public async Task<UserListDto> GetUserById(string userId)
		{
			var user = await _userRepository.FindByIdAsync(userId);
			if (user != null)
			{
				var userDto = new UserListDto()
				{
					Id = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					UserName = user.UserName,
					PhoneNumber = user.PhoneNumber,
					EmailConfirmed = user.EmailConfirmed,
					AccessFailedCount = user.AccessFailedCount
				};
				return userDto;
			}
			return null;
		}

		public async Task<bool> UpdateUserByAdmin(UpdateUserDto updateUserDto)
		{
			var user = await _userRepository.FindByIdAsync(updateUserDto.Id);
			if (user == null)
			{
				return false;
			}
			user.FirstName = updateUserDto.FirstName;
			user.LastName = updateUserDto.LastName;
			user.PhoneNumber = updateUserDto.PhoneNumber;

			var result = await _userRepository.UpdateUser(user);
			return result.Succeeded;
		}

		public async Task<bool> DeleteUserByAdmin(string userId)
		{
			var user = await _userRepository.FindByIdAsync(userId);
			if (user == null)
			{
				return false;
			}
			user.IsDelete = true;
			var result = await _userRepository.UpdateUser(user);
			return result.Succeeded;
		}

		#endregion

		#region UserRole

		public async Task<bool> AddUserRole(AddUserRoleDto addUserRoleDto)
		{
			var user = await _userRepository.FindByIdAsync(addUserRoleDto.UserId);
			if (user == null)
			{
				return false;
			}
			var result = await _userRepository.AddUserRole(user, addUserRoleDto.RoleName);
			return result.Succeeded;

		}
		#endregion
	}
}