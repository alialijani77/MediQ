namespace MediQ.Core.Exceptions
{
	public class NotFoundException : Exception
	{
		#region ctor
		public NotFoundException(Type type, object key)
			: base($"Entity {type} with key {key} was not found.")
		{}
		#endregion
	}
}
