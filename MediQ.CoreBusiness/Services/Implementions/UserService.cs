using MediQ.Core.DTOs.Account.User;
using MediQ.Core.Security;
using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.Domain.Entities.UserManagement;
using MediQ.Domain.Interfaces;

namespace MediQ.CoreBusiness.Services.Implementions
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		#region ctor
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		#endregion
		#region Register
		public async Task<bool> Register(RegisterDto register)
		{
			var existsUser = await _userRepository.IsExistsUserByEmail(register.Email.Trim().ToLower());
			if (existsUser != null)
			{
				return false;
			}
			var password = PasswordHelper.HashPassword(register.Password);

			var user = new User();
			user.Email = register.Email.Trim().ToLower();
			user.UserName = register.Email.Trim().ToLower();
			user.FirstName = register.Email.Trim().ToLower();
			user.LastName = register.Email.Trim().ToLower();
			await _userRepository.CreateUser(user, password);


			return true;
			throw new NotImplementedException();
		}
		#endregion

	}
}
