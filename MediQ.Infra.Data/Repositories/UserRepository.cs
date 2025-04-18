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
		private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

		#region ctor
		public UserRepository(BaseContext context, UserManager<User> userManager,SignInManager<User> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
		{
			var result = await _userManager.ConfirmEmailAsync(user, token);
			return result;
		}
		#endregion
		public async Task<IdentityResult> CreateUser(User user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);
			return result;
		}

		public async Task<User> FindByIdAsync(string userId)
		{
			var result = await _userManager.FindByIdAsync(userId);
			return result;
		}

		public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
		{
			var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
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

		public async Task<User> FindByEmailAsync(string email)
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

		public async Task<IList<Claim>> GetClaimsAsync(User user)
		{
			var result = await _userManager.GetClaimsAsync(user);
			return result;
		}

		public async Task<IList<string>> GetRolesAsync(User user)
		{
			var result = await _userManager.GetRolesAsync(user);
			return result;
		}

		public async Task<IList<User>> GetAllUsers()
		{
			var result =  _userManager.Users.ToList();
			return result;
		}

		public async Task<IdentityResult> UpdateUser(User user)
		{
			var result = await _userManager.UpdateAsync(user);
			return result;
		}

		public async Task<IdentityResult> AddUserRole(User user, string role)
		{
			var result = await _userManager.AddToRoleAsync(user, role);
			return result;
		}

		public async Task<IList<string>> GetAllUserRoles(User user)
		{
			var result = await _userManager.GetRolesAsync(user);
			return result;
		}
	}
}
