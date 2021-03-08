using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccess.Interfaces
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        Task<List<Category>> GetAllWithCategoryBlogsAsync();
    }
}
