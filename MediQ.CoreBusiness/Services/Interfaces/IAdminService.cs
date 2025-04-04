using MediQ.Core.DTOs.Admin.Users;

namespace MediQ.CoreBusiness.Services.Interfaces
{
	public interface IAdminService
	{
		Task<IList<UserListDto>> GetAllUsers();
		Task<UserListDto> GetUserById(string userId);

	}
}
