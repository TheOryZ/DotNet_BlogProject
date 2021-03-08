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
            TempData["active"] = "blog";
            return View(await _blogApiService.GetAllAsync());
        }
        public IActionResult Create()
        {
            TempData["active"] = "blog";
            return View(new BlogAddModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel model)
        {
            TempData["active"] = "blog";
            if(ModelState.IsValid)
            {
                await _blogApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            TempData["active"] = "blog";
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
            TempData["active"] = "blog";
            if(ModelState.IsValid)
            {
                await _blogApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            TempData["active"] = "blog";
            await _blogApiService.DelteAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AssignCategory(int id, [FromServices]ICategoryApiService categoryApiService)
        {
            TempData["active"] = "blog";
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
            TempData["active"] = "blog";
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