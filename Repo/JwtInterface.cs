using Microsoft.AspNetCore.Identity;

namespace NZwalks.API.Repo
{
    public interface JwtInterface
    {
        string CreateJwtToken(IdentityUser user,List<string> roles);
    }
}
