using Hangfire;

namespace MediQ.CoreBusiness.Services.Interfaces
{
	public interface IEmailService
	{
		[Queue("email")]
		[AutomaticRetry(Attempts = 30)]
		Task<bool> SendEmail(string to, string subject, string body);
	}
}