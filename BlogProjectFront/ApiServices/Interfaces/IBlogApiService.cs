using System.Collections.Generic;
using System.Threading.Tasks;
using BlogProjectFront.Models;

namespace BlogProjectFront.ApiServices.Interfaces
{
    public interface IBlogApiService
    {
        Task<List<BlogListModel>> GetAllAsync();
        Task<BlogListModel> GetByIdAsync(int id);
        Task<List<BlogListModel>> GetAllByCategoryIdAsync(int id);
        Task AddAsync(BlogAddModel model);
        Task UpdateAsync(BlogUpdateModel model);
        Task DelteAsync(int id);
        Task<List<CommentListModel>> GetCommentsAsync(int blogId, int? parentCommentId);
        Task AddToCommentAsync(CommentAddModel model);
        Task<List<CategoryListModel>> GetCategoriesAsync(int blogId);
        Task<List<BlogListModel>> GetLastFiveAsync();
        Task<List<BlogListModel>> SearchAsync(string s);
        Task AddToCategoryAsync(CategoryBlogModel model);
        Task RemoveFromCategoryAsync(CategoryBlogModel model);
    }
}