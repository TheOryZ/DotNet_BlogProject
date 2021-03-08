using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccess.Interfaces
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId);
        Task<List<Category>> GetcategoriesAsync(int blogId);
        Task<List<Blog>> GetLastFiveBlogsAsync();
    }
}
