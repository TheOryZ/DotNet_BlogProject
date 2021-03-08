using System.Threading.Tasks;
using BlogProjectFront.Models;

namespace BlogProjectFront.ApiServices.Interfaces
{
    public interface IAuthApiService
    {
        Task<bool> SignIn(AppUserSignInModel model);
    }
}