using MediQ.Domain.Entities.UserManagement;

namespace MediQ.Domain.Interfaces
{
	public interface IRoleRepository
	{
		Task<IList<Role>> GetAllRoles();

	}
}