namespace MediQ.Core.DTOs.Admin.Users
{
	public class UserListDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string EmailConfirmed { get; set; }
		public string AccessFailedCount { get; set; }
	}
}
