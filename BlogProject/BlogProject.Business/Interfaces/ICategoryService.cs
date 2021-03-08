using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Interfaces
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<List<Category>> GetAllSortedByIdAsync();
        Task<List<Category>> GetAllWithCategoryBlogsAsync();
    }
}
