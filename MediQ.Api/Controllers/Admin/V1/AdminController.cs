using Asp.Versioning;
using MediQ.Core.DTOs.Account.Role;
using MediQ.Core.DTOs.Account.User;
using MediQ.Core.DTOs.Admin.Users;
using MediQ.Core.DTOs.ApiResult;
using MediQ.CoreBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediQ.Api.Controllers.Admin.V1
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1")]
	[ApiController]
	public class AdminController : ControllerBase
	{

		private readonly IAdminService _adminService;
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;


		#region ctor
		public AdminController(IAdminService adminService, IUserService userService, IRoleService roleService)
		{
			_adminService = adminService;
			_userService = userService;
			_roleService = roleService;
		}
		#endregion

		#region Users
		[HttpPost("GetAllUsers")]
		public virtual async Task<IActionResult> GetAllUsers()
		{
			var result = await _adminService.GetAllUsers();
			if (result != null)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(result, true, ""));

			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}

		[HttpPost("GetUserById")]
		public virtual async Task<IActionResult> GetUserById([FromBody] string userId)
		{
			var result = await _adminService.GetUserById(userId);
			if (result != null)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(result, true, ""));

			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}

		[HttpPost("CreateUser")]
		public virtual async Task<IActionResult> CreateUser(RegisterDto register)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception(StatusCodes.Status403Forbidden.ToString());

			}
			var result = await _userService.Register(register);

			if (result)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(true, true, "ایمیل فعالسازی به کاربر افزوده شده ارسال شد."));
			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}

		[HttpPost("UpdateUser")]
		public virtual async Task<IActionResult> UpdateUser(UpdateUserDto updateuser)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception(StatusCodes.Status403Forbidden.ToString());

			}
			var result = await _adminService.UpdateUserByAdmin(updateuser);

			if (result)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(true, true, "کاربر آپدیت شد."));
			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}

        [HttpPost("DeleteUserById")]
        public virtual async Task<IActionResult> DeleteUserById([FromBody] string userId)
        {
            var result = await _adminService.DeleteUserByAdmin(userId);
            if (result)
            {
                return new JsonResult(ApiResultDto<bool>.CreateSuccess(true, true, "کاربر حذف شد."));
            }
            throw new Exception(StatusCodes.Status404NotFound.ToString());
        }
		#endregion

		#region Roles
		[HttpPost("GetAllUsers")]
		public virtual async Task<IActionResult> GetAllRoles()
		{
			var result = await _roleService.GetAllRoles();
			if (result != null)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(result, true, ""));

			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}

		[HttpPost("CreateRole")]
		public virtual async Task<IActionResult> CreateRole(AddNewRoleDto newRoleDto)
		{
			var result = await _roleService.CreateRole(newRoleDto);
			if (result != null)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(result, true, ""));

			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}

        [HttpPost("UpdateRole")]
        public virtual async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var result = await _roleService.UpdateRole(updateRoleDto);
            if (result)
            {
                return new JsonResult(ApiResultDto<bool>.CreateSuccess(result, true, ""));

            }
            throw new Exception(StatusCodes.Status404NotFound.ToString());
        }
		#endregion

		#region UserRole
		[HttpPost("AddUserRole")]
		public virtual async Task<IActionResult> AddUserRole(AddUserRoleDto addUserRole)
		{
			var result = await _adminService.AddUserRole(addUserRole);
			if (result)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(result, true, ""));

			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}
		#endregion
	}
}