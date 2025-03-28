using MediQ.Domain.Entities.Common;
using MediQ.Domain.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MediQ.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<IdentityResult> CreateUser(IdentityUser user, string password);
		Task<IdentityUser> IsExistsUserByEmail(string email);
		Task<EmailSetting> GetDefaultEmail();
		Task<string> GenerateChangeEmailTokenAsync(IdentityUser user, string email);
		Task<IdentityUser> FindByIdAsync(string userId);
		Task<IdentityResult> ConfirmEmailAsync(IdentityUser user,string token);
		Task<IdentityUser> FindByEmailAsync(string email);
		Task<SignInResult> PasswordSignIn(string username, string password, bool isPersistent);
		Task<IList<Claim>> GetClaimsAsync(IdentityUser user);
		Task<IList<string>> GetRolesAsync(IdentityUser user);
		Task SignOutAsync();

	}
}
