using Microsoft.AspNetCore.Identity;

namespace MediQ.Domain.Entities.UserManagement
{
	public class User : IdentityUser<int>
	{
		#region Properties
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool IsDelete { get; set; } = false;
		#endregion
	}
}