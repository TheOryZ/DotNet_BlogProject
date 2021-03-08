using System.Threading.Tasks;

namespace BlogProjectFront.ApiServices.Interfaces
{
    public interface IImageApiService
    {
        Task<string> GetBlogImageByIdAsync(int id);
    }
}