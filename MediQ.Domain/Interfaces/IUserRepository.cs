using MediQ.Domain.Entities.Common;
using MediQ.Domain.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MediQ.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<IdentityResult> CreateUser(User user, string password);
		Task<IdentityUser> IsExistsUserByEmail(string email);
		Task<EmailSetting> GetDefaultEmail();
		Task<string> GenerateChangeEmailTokenAsync(User user, string email);
		Task<User> FindByIdAsync(string userId);
		Task<IdentityResult> ConfirmEmailAsync(User user,string token);
		Task<User> FindByEmailAsync(string email);
		Task<SignInResult> PasswordSignIn(string username, string password, bool isPersistent);
		Task<IList<Claim>> GetClaimsAsync(User user);
		Task<IList<string>> GetRolesAsync(User user);
		Task SignOutAsync();
		Task<IList<User>> GetAllUsers();
		Task<IdentityResult> UpdateUser(User user);
		Task<IdentityResult> AddUserRole(User user, string role);



	}
}
