using Microsoft.AspNetCore.Identity;

namespace MediQ.Domain.Entities.UserManagement
{
	public class User : IdentityUser
	{
		#region Properties
		public string FirstName { get; set; }
		public string LastName { get; set; }
		#endregion
	}
}
