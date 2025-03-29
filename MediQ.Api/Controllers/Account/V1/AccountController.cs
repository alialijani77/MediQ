using Asp.Versioning;
using MediQ.Core.DTOs.Account.User;
using MediQ.Core.DTOs.ApiResult;
using MediQ.CoreBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediQ.Api.Controllers.Account.V1
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1")]
	[ApiController]

	public class AccountController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly ILogger<AccountController> _logger;
		#region ctor
		public AccountController(IUserService userService, ILogger<AccountController> logger)
		{
			_userService = userService;
			_logger = logger;
		}
		#endregion

		#region Register
		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterDto register)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception(StatusCodes.Status403Forbidden.ToString());

			}
			var result = await _userService.Register(register);

			if (result)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(true, true, "ایمیل فعالسازی ارسال شد"));
			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}
		#endregion

		#region Login
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception(StatusCodes.Status403Forbidden.ToString());
			}
			var token = await _userService.Login(loginDto);


			if (!string.IsNullOrEmpty(token))
			{
				return Ok(new { token });
			}
			return Unauthorized();
		}
		#endregion
	}
}