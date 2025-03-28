using MediQ.Domain.Entities.Common;
using MediQ.Domain.Entities.UserManagement;
using MediQ.Domain.Interfaces;
using MediQ.Infra.Data.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace MediQ.Infra.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly BaseContext _context;
		private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<User> _signInManager;

		#region ctor
		public UserRepository(BaseContext context, UserManager<IdentityUser> userManager,SignInManager<User> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string token)
		{
			var result = await _userManager.ConfirmEmailAsync(user, token);
			return result;
		}
		#endregion
		public async Task<IdentityResult> CreateUser(IdentityUser user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);
			return result;
		}

		public async Task<IdentityUser> FindByIdAsync(string userId)
		{
			var result = await _userManager.FindByIdAsync(userId);
			return result;
		}

		public async Task<string> GenerateChangeEmailTokenAsync(IdentityUser user, string email)
		{
			var result = await _userManager.GenerateChangeEmailTokenAsync(user, email);
			return result;
		}

		public async Task<EmailSetting> GetDefaultEmail()
		{
			return await _context.EmailSettings.FirstOrDefaultAsync(e => e.IsDefault && !e.IsDelete);
		}

		public async Task<IdentityUser> IsExistsUserByEmail(string email)
		{
			var result = await _userManager.FindByEmailAsync(email);
			return result;
		}

		public async Task<IdentityUser> FindByEmailAsync(string email)
		{
			var result = await _userManager.FindByEmailAsync(email);
			return result;
		}
		public async Task<SignInResult> PasswordSignIn(string username,string password,bool isPersistent)
		{
			var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, false);
			return result;
		}

		public async Task SignOutAsync()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
		{
			var result = await _userManager.GetClaimsAsync(user);
			return result;
		}

		public async Task<IList<string>> GetRolesAsync(IdentityUser user)
		{
			var result = await _userManager.GetRolesAsync(user);
			return result;

		}
	}
}
