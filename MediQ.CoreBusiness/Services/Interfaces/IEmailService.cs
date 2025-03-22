﻿namespace MediQ.CoreBusiness.Services.Interfaces
{
	public interface IEmailService
	{
		Task<bool> SendEmail(string to, string subject, string body);
	}
}