using MediQ.Core.DTOs.Account.User;
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
		private readonly ITokenService _tokenService;

		#region ctor
		public UserService(IUserRepository userRepository, IEmailService emailService, ITokenService tokenService)
		{
			_userRepository = userRepository;
			_emailService = emailService;
			_tokenService = tokenService;
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

			var user = new User();
			user.Email = register.Email.Trim().ToLower();
			user.UserName = register.Email.Trim().ToLower();
			user.FirstName = register.Email.Trim().ToLower();
			user.LastName = register.Email.Trim().ToLower();
			var result = await _userRepository.CreateUser(user, register.Password);

			if (result.Succeeded)
			{
				var token = await _userRepository.GenerateEmailConfirmationTokenAsync(user);

				#region SendActivationEmail

				var body = $@"
                <div> برای فعالسازی حساب کاربری خود روی لینک زیر کلیک کنید . </div>
                <a href='{PathTools.Root}/api/v1/Account/Activate-Email?activationCode={token}&userId={user.Id}'>فعالسازی حساب کاربری</a>";

				var emailResult = await _emailService.SendEmail(user.Email, "فعالسازی حساب کاربری", body);

				if (emailResult)
					return true;
				#endregion
			}
			return false;
		}
		#endregion
		#region Login
		public async Task<string> Login(LoginDto loginDto)
		{
			await _userRepository.SignOutAsync();

			var result = await _userRepository.PasswordSignIn(loginDto.Email, loginDto.Password, loginDto.IsPersistent);
			if (result.Succeeded)
			{
				var user = await _userRepository.FindByEmailAsync(loginDto.Email);
				var token = await _tokenService.GenerateToken(user);

				return token;
			}
			return null;
		}
		#endregion
		#region EmailActivation
		public async Task<bool> EmailActivation(string activationcode, string userId)
		{
			var user = await _userRepository.FindByIdAsync(userId);
			if (user == null)
			{
				return false;
			}
			activationcode = activationcode.Replace(" ", "+");
			var result = await _userRepository.ConfirmEmailAsync(user, activationcode);
			if (result.Succeeded) return true;
			return false;
		}
		#endregion

		#region ForgotPassword

		public async Task<bool> ForgotPassword(ForgotPasswordConfirmationDto forgotPassword)
		{
			var user = await _userRepository.FindByEmailAsync(forgotPassword.Email);
			if (user == null)
			{
				return false;
			}
			if (!await _userRepository.IsEmailConfirmedAsync(user))
			{
				return false;
			}
			var token = await _userRepository.GeneratePasswordResetTokenAsync(user);

			#region SendActivationEmail

			var body = $@"
                <div>برای بازیابی رمز عبورروی لینک زیر کلیک کنید.</div>
                <a href='{PathTools.Root}/api/v1/Account/ResetPassword?token={token}&userId={user.Id}'>بازیابی رمز عبور</a>";

			var emailResult = await _emailService.SendEmail(user.Email, "بازیابی رمز عبور", body);

			if (emailResult)
				return true;

			return false;
			#endregion

		}
		#endregion
	}
}
