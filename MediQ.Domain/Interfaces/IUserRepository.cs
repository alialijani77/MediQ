using MediQ.Domain.Entities.Common;
using MediQ.Domain.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;

namespace MediQ.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<IdentityResult> CreateUser(User user, string password);
		Task<User> IsExistsUserByEmail(string email);
		Task<EmailSetting> GetDefaultEmail();
		Task<string> GenerateChangeEmailTokenAsync(User user, string email);
		Task<User> FindByIdAsync(string userId);
		Task<IdentityResult> ConfirmEmailAsync(User user,string token);

	}
}
