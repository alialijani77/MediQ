namespace MediQ.Domain.Entities.Common
{
	public class EmailSetting : BaseEntity
	{
		public string From { get; init; }

		public string Password { get; init; }

		public string SMTP { get; init; }

		public bool EnableSSL { get; init; }

		public string DisplayName { get; init; }

		public int Port { get; init; }

		public bool IsDefault { get; init; }
	}
}
