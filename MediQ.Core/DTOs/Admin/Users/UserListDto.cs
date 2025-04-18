﻿namespace MediQ.Core.DTOs.Admin.Users
{
	public class UserListDto
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public bool EmailConfirmed { get; set; }
		public int AccessFailedCount { get; set; }
	}
}
