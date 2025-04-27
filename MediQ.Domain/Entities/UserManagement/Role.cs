using Microsoft.AspNetCore.Identity;

namespace MediQ.Domain.Entities.UserManagement
{
	public class Role : IdentityRole<int>
	{
		public string Description { get; set; }
	}
}
