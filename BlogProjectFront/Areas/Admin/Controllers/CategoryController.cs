using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectFront.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApiService _categoryApiService;
        public CategoryController(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["active"] = "category";
            return View(await _categoryApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            TempData["active"] = "category";
            return View(new CategoryAddModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddModel model)
        {
            TempData["active"] = "category";
            if(ModelState.IsValid)
            {
                await _categoryApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            TempData["active"] = "category";
            var categoryList = await _categoryApiService.GetByIdAsync(id);
            return View(new CategoryUpdateModel {
                Id = categoryList.Id,
                Name = categoryList.Name
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateModel model)
        {
            TempData["active"] = "category";
            if(ModelState.IsValid)
            {
                await _categoryApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["active"] = "category";
            await _categoryApiService.DelteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction("Index","Home", new {area=""});
        }
    }
}