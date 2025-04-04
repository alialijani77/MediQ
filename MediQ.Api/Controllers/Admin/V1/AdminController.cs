﻿using Asp.Versioning;
using MediQ.Core.DTOs.Account.User;
using MediQ.Core.DTOs.Admin.Users;
using MediQ.Core.DTOs.ApiResult;
using MediQ.CoreBusiness.Services.Implementions;
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


		#region ctor
		public AdminController(IAdminService adminService, IUserService userService)
		{
			_adminService = adminService;
			_userService = userService;
		}
		#endregion

		#region Users
		[HttpPost("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			var result = await _adminService.GetAllUsers();
			if (result != null)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(result, true, ""));

			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}

		[HttpPost("GetUserById")]
		public async Task<IActionResult> GetUserById([FromBody]string userId)
		{
			var result = await _adminService.GetUserById(userId);
			if (result != null)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(result, true, ""));

			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}

		[HttpPost("CreateUser")]
		public async Task<IActionResult> CreateUser(RegisterDto register)
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
		public async Task<IActionResult> UpdateUser(UpdateUserDto updateuser)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception(StatusCodes.Status403Forbidden.ToString());

			}
			var result = await _userService.Register(updateuser);

			if (result)
			{
				return new JsonResult(ApiResultDto<bool>.CreateSuccess(true, true, "ایمیل فعالسازی به کاربر افزوده شده ارسال شد."));
			}
			throw new Exception(StatusCodes.Status404NotFound.ToString());
		}
		#endregion
	}
}