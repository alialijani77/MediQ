using MediQ.Domain.Entities.Common;
using MediQ.Domain.Entities.UserManagement;
using MediQ.Domain.Interfaces;
using MediQ.Infra.Data.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediQ.Infra.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly BaseContext _context;
		private readonly UserManager<User> _userManager;
		#region ctor
		public UserRepository(BaseContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		#endregion
		public async Task<IdentityResult> CreateUser(User user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);
			return result;
		}

		public async Task<string> GenerateChangeEmailTokenAsync(User user, string email)
		{
			var result = await _userManager.GenerateChangeEmailTokenAsync(user, email);
			return result;
		}

		public async Task<EmailSetting> GetDefaultEmail()
		{
			return await _context.EmailSettings.FirstOrDefaultAsync(e => e.IsDefault && !e.IsDelete);
		}

		public async Task<User> IsExistsUserByEmail(string email)
		{
			var result = await _userManager.FindByEmailAsync(email);
			return result;
		}

		
	}
}
