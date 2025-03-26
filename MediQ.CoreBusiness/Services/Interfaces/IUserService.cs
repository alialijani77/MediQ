using MediQ.Core.DTOs.Account.User;

namespace MediQ.CoreBusiness.Services.Interfaces
{
	public interface IUserService
	{
		Task<bool> Register(RegisterDto register);
	}
}
