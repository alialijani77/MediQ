namespace MediQ.Domain.Entities.Common
{
	public class Visitor
	{
		public string Ip { get; set; }
		public string CurrentLink { get; set; }
		public string ReferrerLink { get; set; }
		public string Method { get; set; }
		public string Protocol { get; set; }
		public string PhysicalPath { get; set; }
		public VisitorVersion Browser { get; set; }
		public VisitorVersion OperationSystem { get; set; }
		public Device Device { get; set; }

	}

	public class VisitorVersion
	{
		public string Family { get; set; }
		public string Version { get; set; }
	}

	public class Device
	{
		public string Brand { get; set; }
		public string Family { get; set; }
		public string Model { get; set; }
		public bool IsSpider { get; set; }

	}
}
