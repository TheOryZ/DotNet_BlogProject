using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogProject.Business.Interfaces;
using BlogProject.Business.Tools.LogTool;
using BlogProject.DTO.Dtos.CategoryDtos;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly ICustomLogger _customLogger;
        public CategoriesController(IMapper mapper, ICategoryService categoryService, ICustomLogger customLogger)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _customLogger = customLogger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<CategoryListDto>>(await _categoryService.GetAllSortedByIdAsync()));
        }
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<CategoryListDto>(await _categoryService.FindByIdAsync(id)));
        }
        [HttpPost]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> Create(CategoryAddDto categoryAddDto)
        {
            await _categoryService.AddAsync(_mapper.Map<Category>(categoryAddDto));
            return Created("", categoryAddDto);
        }
        [HttpPut("{id}")]
        [Authorize]
        [ValidModel]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto categoryUpdateDto)
        {
            if (id != categoryUpdateDto.Id)
                return BadRequest("Invalid id entered");
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryUpdateDto));
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.RemoveAsync(await _categoryService.FindByIdAsync(id));
            return NoContent();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetWithBlogsCount()
        {
            var categories = await _categoryService.GetAllWithCategoryBlogsAsync();
            List<CategoryWithBlogsCountDto> listCategory = new List<CategoryWithBlogsCountDto>();
            foreach (var item in categories)
            {
                CategoryWithBlogsCountDto categoryWithBlogsCountDtos = new CategoryWithBlogsCountDto
                {
                    CategoryName = item.Name,
                    CategoryId = item.Id,
                    BlogsCount = item.CategoryBlogs.Count
                };

                listCategory.Add(categoryWithBlogsCountDtos);
            }
            return Ok(listCategory);
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _customLogger.LogError($"\nWhere the error occurred : {errorInfo.Path}\n Error Message : {errorInfo.Error.Message}\n Stack Trace : {errorInfo.Error.StackTrace}");
            return Problem(detail: "Something went wrong. It will be fixed as soon as possible.");
        }
    }
}
