using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace MediQ.Api.Controllers.Account.V2
{
	[ApiVersion("2")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class AccountController : V1.AccountController
	{
		public override IEnumerable<string> Get()
		{
			return new string[] { "V2 value1", "V2 value2", "V2 value3" };
		}
	}
}
