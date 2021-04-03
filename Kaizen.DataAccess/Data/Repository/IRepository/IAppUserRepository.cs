using System.Threading.Tasks;
using Kaizen.Models;

namespace Kaizen.DataAccess.Data.Repository.IRepository
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<UserSuccessResponse> Login(UserLogin userlogin);
        Task<UserSuccessResponse> Register(UserRegister userregister);
        Task<AppUser> GetCurrentLoginDetails();

    }
}