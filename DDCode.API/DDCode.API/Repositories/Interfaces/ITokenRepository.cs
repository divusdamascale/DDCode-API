using Microsoft.AspNetCore.Identity;

namespace DDCode.API.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
