using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.Domain.Interfaces;
using System.Net.Mail;

namespace MediQ.CoreBusiness.Services.Implementions
{
	public class EmailService : IEmailService
	{
		private readonly IUserRepository _userRepository;
		#region ctor
		public EmailService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		#endregion

		#region SendEmail
		public async Task<bool> SendEmail(string to, string subject, string body)
		{
			try
			{
				var defaultSiteEmail = await _userRepository.GetDefaultEmail();

				MailMessage mail = new MailMessage();
				SmtpClient SmtpServer = new SmtpClient(defaultSiteEmail.SMTP);

				mail.From = new MailAddress(defaultSiteEmail.From, defaultSiteEmail.DisplayName);
				mail.To.Add(to);
				mail.Subject = subject;
				mail.Body = body;
				mail.IsBodyHtml = true;

				if (defaultSiteEmail.Port != 0)
				{

					SmtpServer.Port = defaultSiteEmail.Port;
					SmtpServer.EnableSsl = defaultSiteEmail.EnableSSL;

				}

				SmtpServer.Credentials = new System.Net.NetworkCredential(defaultSiteEmail.From, defaultSiteEmail.Password);
				SmtpServer.Send(mail);

				return true;
			}
			catch (Exception exception)
			{
				return false;
			}
		}
		#endregion
	}
}