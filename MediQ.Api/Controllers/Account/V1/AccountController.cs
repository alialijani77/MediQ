using Asp.Versioning;
using MediQ.Core.DTOs.Account.User;
using MediQ.Core.DTOs.ApiResult;
using MediQ.CoreBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediQ.Api.Controllers.Account.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        #region ctor
        public AccountController(IUserService userService, ILogger<AccountController> logger, ITokenService tokenService)
        {
            _userService = userService;
            _logger = logger;
            _tokenService = tokenService;
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
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                var token = await _tokenService.GenerateToken(user);
                return Ok(new { token });
            }

            return Unauthorized();
        }
        #endregion
    }
}