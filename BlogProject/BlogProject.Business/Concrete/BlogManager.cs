using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.Dtos.CategoryBlogDtos;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IGenericDal<Blog> _genericDal;
        private readonly IGenericDal<CategoryBlog> _categoryBlogDal;
        private readonly IBlogDal _blogDal;
        public BlogManager(IGenericDal<Blog> genericDal, IGenericDal<CategoryBlog> categoryBlogDal, IBlogDal blogDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _categoryBlogDal = categoryBlogDal;
            _blogDal = blogDal;
        }

        public async Task<List<Blog>> GetAllSortedByPostedTimeAsync()
        {
            return await _genericDal.GetAllAsync(I => I.PostedTime);
        }

        public async Task AddToCategoryAsync(CategoryBlogDto categoryBlogDto)
        {
            var controlCategoryBlog = await _categoryBlogDal.GetAsync(I => I.CategoryId == categoryBlogDto.CategoryId &&
            I.BlogId == categoryBlogDto.BlogId);

            if(controlCategoryBlog == null)
            {
                await _categoryBlogDal.AddAsync(new CategoryBlog
                {
                    BlogId = categoryBlogDto.BlogId,
                    CategoryId = categoryBlogDto.CategoryId
                });
            }
        }

        public async Task RemoveFromCategoryAsync(CategoryBlogDto categoryBlogDto)
        {
            var deletedCategoryBlog = await _categoryBlogDal.GetAsync(I => I.CategoryId == categoryBlogDto.CategoryId && 
            I.BlogId == categoryBlogDto.BlogId);
            if(deletedCategoryBlog != null)
            {
                await _categoryBlogDal.RemoveAsync(deletedCategoryBlog);
            }
        }

        public async Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await _blogDal.GetAllByCategoryIdAsync(categoryId);
        }

        public async Task<List<Category>> GetcategoriesAsync(int blogId)
        {
            return await _blogDal.GetcategoriesAsync(blogId);
        }

        public async Task<List<Blog>> GetLastFiveBlogsAsync()
        {
            return await _blogDal.GetLastFiveBlogsAsync();
        }

        public async Task<List<Blog>> SearchAsync(string searchString)
        {
           return await _blogDal.GetAllAsync(I => I.Title.Contains(searchString) || I.ShortDescription.Contains(searchString) ||
                                             I.Description.Contains(searchString), I => I.PostedTime);
        }
    }
}
