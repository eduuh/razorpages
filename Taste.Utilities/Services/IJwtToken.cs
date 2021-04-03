using Kaizen.Models;

namespace Kaizen.Utilities.Services
{
    public interface IJwtToken
    {
        string createToken(AppUser user);
    }
}