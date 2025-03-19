using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace MediQ.Api.Controllers.Account.V1
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1")]
	[ApiController]

	public class AccountController : ControllerBase
	{
		[HttpGet]
		public virtual IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}
	}
}