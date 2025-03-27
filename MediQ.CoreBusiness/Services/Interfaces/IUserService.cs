using MediQ.Core.DTOs.Account.User;

namespace MediQ.CoreBusiness.Services.Interfaces
{
	public interface IUserService
	{
		Task<bool> Register(RegisterDto register);
		Task<bool> Login(LoginDto loginDto);
		Task<bool> EmailActivation(string activationcode);

	}
}
