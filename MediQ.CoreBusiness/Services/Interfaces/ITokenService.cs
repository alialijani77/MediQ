using Microsoft.AspNetCore.Identity;

namespace MediQ.CoreBusiness.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(IdentityUser user);
    }
}
