using MediQ.Domain.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;

namespace MediQ.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<IdentityResult> CreateUser(User user, string password);
		Task<User> IsExistsUserByEmail(string email);
	}
}
