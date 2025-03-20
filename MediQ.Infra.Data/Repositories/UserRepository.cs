using MediQ.Domain.Entities.UserManagement;
using MediQ.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MediQ.Infra.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<User> _userManager;
		#region ctor
		public UserRepository(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		#endregion
		public async Task<IdentityResult> CreateUser(User user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);
			return result;
		}

		public async Task<User> IsExistsUserByEmail(string email)
		{
			var result = await _userManager.FindByEmailAsync(email);
			return result;
		}

		
	}
}
