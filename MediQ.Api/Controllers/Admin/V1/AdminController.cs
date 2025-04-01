using Asp.Versioning;
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

		#region ctor
		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}
		#endregion

		#region Users
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