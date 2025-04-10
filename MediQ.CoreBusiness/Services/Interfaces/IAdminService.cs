using MediQ.Core.DTOs.Account.User;
using MediQ.Core.DTOs.Admin.Users;

namespace MediQ.CoreBusiness.Services.Interfaces
{
	public interface IAdminService
	{
		Task<IList<UserListDto>> GetAllUsers();
		Task<UserListDto> GetUserById(string userId);
		Task<bool> UpdateUserByAdmin(UpdateUserDto updateUserDto);
		Task<bool> DeleteUserByAdmin(string userId);
	}
}
