using MediQ.Core.DTOs.Account.User;
using MediQ.Core.Security;
using MediQ.Core.Statics;
using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.Domain.Entities.UserManagement;
using MediQ.Domain.Interfaces;

namespace MediQ.CoreBusiness.Services.Implementions
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IEmailService _emailService;
		#region ctor
		public UserService(IUserRepository userRepository, IEmailService emailService)
		{
			_userRepository = userRepository;
			_emailService = emailService;
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
			var result = await _userRepository.CreateUser(user, password);

			if(result.Succeeded)
			{
				var token = _userRepository.GenerateChangeEmailTokenAsync(user, register.Email);

				#region SendActivationEmail

				var body = $@"
                <div> برای فعالسازی حساب کاربری خود روی لینک زیر کلیک کنید . </div>
                <a href='{PathTools.Root}/Activate-Email/{token}'>فعالسازی حساب کاربری</a>";

				await _emailService.SendEmail(user.Email, "فعالسازی حساب کاربری", body);

				#endregion
			}
			return true;
		}
		#endregion

	}
}
