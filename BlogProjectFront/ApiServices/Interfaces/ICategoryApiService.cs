using System.Collections.Generic;
using System.Threading.Tasks;
using BlogProjectFront.Models;

namespace BlogProjectFront.ApiServices.Interfaces
{
    public interface ICategoryApiService
    {
        Task<List<CategoryListModel>> GetAllAsync();
        Task<List<CategoryWithBlogsCountModel>> GetAllWithBlogsCountAsync();
        Task<CategoryListModel> GetByIdAsync(int id);
        Task AddAsync(CategoryAddModel model);
        Task UpdateAsync(CategoryUpdateModel model);
        Task DelteAsync(int id);
    }
}