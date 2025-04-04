using MediQ.Core.DTOs.Admin.Users;
using MediQ.CoreBusiness.Services.Interfaces;
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
	}
}
