using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Filters;
using BlogProjectFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectFront.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        public BlogController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        [JWtAuthorize]
        public async Task<IActionResult> Index()
        {
            return View(await _blogApiService.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View(new BlogAddModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel model)
        {
            if(ModelState.IsValid)
            {
                await _blogApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var blogList = await _blogApiService.GetByIdAsync(id);
            return View(new BlogUpdateModel {
                Id = blogList.Id,
                Title = blogList.Title,
                Description = blogList.Description,
                ShortDescription = blogList.ShortDescription
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateModel model)
        {
            if(ModelState.IsValid)
            {
                await _blogApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _blogApiService.DelteAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AssignCategory(int id, [FromServices]ICategoryApiService categoryApiService)
        {
            var categories = await categoryApiService.GetAllAsync();
            //var blogCategories = (await _blogApiService.GetCategoriesAsync(id)).Select(I=>I.Name).ToList();
            var blogCategories = await _blogApiService.GetCategoriesAsync(id);

            TempData["blogId"] = id;

            List<AssignCategoryModel> list = new List<AssignCategoryModel>();
            foreach (var item in categories)
            {
                AssignCategoryModel model = new AssignCategoryModel();
                model.CategoryId = item.Id;
                model.CategoryName = item.Name;
                model.Exists = blogCategories.Contains(item);

                list.Add(model);
            }
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCategory(List<AssignCategoryModel> list)
        {
            int id = (int)TempData["blogId"];
            foreach (var item in list)
            {
                if(item.Exists)
                {
                    CategoryBlogModel model = new CategoryBlogModel();
                    model.BlogId = id;
                    model.CategoryId = item.CategoryId;

                    await _blogApiService.AddToCategoryAsync(model);
                }
                else
                {
                    CategoryBlogModel model = new CategoryBlogModel();
                    model.BlogId = id;
                    model.CategoryId = item.CategoryId;

                    await _blogApiService.RemoveFromCategoryAsync(model);
                }
            }
            return RedirectToAction("Index");
        }
    }
}