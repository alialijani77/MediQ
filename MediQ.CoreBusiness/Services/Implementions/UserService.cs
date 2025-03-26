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
				var token = await _userRepository.GenerateChangeEmailTokenAsync(user, register.Email);
				token += $"#userId={user.Id}";

				#region SendActivationEmail

				var body = $@"
                <div> برای فعالسازی حساب کاربری خود روی لینک زیر کلیک کنید . </div>
                <a href='{PathTools.Root}/api/v1/Account/Activate-Email?activationcode={token}'>فعالسازی حساب کاربری</a>";

				var emailResult = await _emailService.SendEmail(user.Email, "فعالسازی حساب کاربری", body);
				
				if(emailResult) return true;
				#endregion
			}
			return false;
		}
		#endregion
		#region EmailActivation
		public async Task<bool> EmailActivation(string activationcode)
		{
			var indexOfUserId = activationcode.IndexOf("#userId");
			var userId = activationcode.Substring(indexOfUserId + 1);
			var token = activationcode.Substring(0, indexOfUserId - 7);
			var user = await _userRepository.FindByIdAsync(userId);
			if (user == null)
			{
				return false;
			}
			var result = _userRepository.ConfirmEmailAsync(user, token);
			if (result.IsCompletedSuccessfully) return true;
			return false;
		}
		#endregion

	}
}
