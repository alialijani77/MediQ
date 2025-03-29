using Asp.Versioning;
using MediQ.CoreBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediQ.Api.Controllers.Admin.V1
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IAdminService _adminService;

		#region ctor
		public UsersController(IAdminService adminService)
		{
			_adminService = adminService;
		}
		#endregion

		#region GetAllUsers
		[HttpPost("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			var result = await _adminService.GetAllUsers();

			return Ok(result);
			//throw new Exception(StatusCodes.Status404NotFound.ToString());
		}
		#endregion
	}
}
