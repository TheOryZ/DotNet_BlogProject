using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogProject.Business.Interfaces;
using BlogProject.Business.Tools.FacadeTool;
using BlogProject.DTO.Dtos.BlogDtos;
using BlogProject.DTO.Dtos.CategoryBlogDtos;
using BlogProject.DTO.Dtos.CategoryDtos;
using BlogProject.DTO.Dtos.CommentDtos;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.CustomFilters;
using BlogProject.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly IFacade _facade;
        public BlogsController(IBlogService blogService, IMapper mapper, ICommentService commentService, IFacade facade)
        {
            _blogService = blogService;
            _mapper = mapper;
            _commentService = commentService;
            _facade = facade;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(_facade.MemoryCache.TryGetValue("blogList",out List<BlogListDto> list))
            {
                return Ok(list);
            }
            var blogs = _mapper.Map<List<BlogListDto>>(await _blogService.GetAllSortedByPostedTimeAsync());
            _facade.MemoryCache.Set("blogList", blogs, new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                Priority = CacheItemPriority.Normal
            });

            return Ok(blogs);
        }
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<BlogListDto>(await _blogService.FindByIdAsync(id)));
        }
        [HttpPost]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> Create([FromForm]BlogAddModel blogAddModel)
        {
            var uploadModel = await UploadFileAsync(blogAddModel.Image, "image/jpeg");
            if(uploadModel.UploadState == Enums.UploadState.Success)
            {
                blogAddModel.ImagePath = uploadModel.NewName;
                await _blogService.AddAsync(_mapper.Map<Blog>(blogAddModel));
                return Created("", blogAddModel);
            }
            else if(uploadModel.UploadState == Enums.UploadState.NotExist)
            {
                await _blogService.AddAsync(_mapper.Map<Blog>(blogAddModel));
                return Created("", blogAddModel);
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        [ValidModel]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> Update(int id, [FromForm]BlogUpdateModel blogUpdateModel)
        {
            if (id != blogUpdateModel.Id)
                return BadRequest("Invalid id entered");

            var uploadModel = await UploadFileAsync(blogUpdateModel.Image, "image/jpeg");
            if (uploadModel.UploadState == Enums.UploadState.Success)
            {
                var updatedBlog = await _blogService.FindByIdAsync(blogUpdateModel.Id);
                updatedBlog.ShortDescription = blogUpdateModel.ShortDescription;
                updatedBlog.Title = blogUpdateModel.Title;
                updatedBlog.Description = blogUpdateModel.Description;
                updatedBlog.ImagePath = uploadModel.NewName;
                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else if (uploadModel.UploadState == Enums.UploadState.NotExist)
            {
                var updatedBlog = await _blogService.FindByIdAsync(blogUpdateModel.Id);
                updatedBlog.ShortDescription = blogUpdateModel.ShortDescription;
                updatedBlog.Title = blogUpdateModel.Title;
                updatedBlog.Description = blogUpdateModel.Description;
                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.RemoveAsync(await _blogService.FindByIdAsync(id));
            return NoContent();
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> AddToCategory(CategoryBlogDto categoryBlogDto)
        {
            await _blogService.AddToCategoryAsync(categoryBlogDto);
            return Created("", categoryBlogDto);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveFromCategory([FromQuery]CategoryBlogDto categoryBlogDto)
        {
            await _blogService.RemoveFromCategoryAsync(categoryBlogDto);
            return NoContent();
        }
        [HttpGet("[action]/{id}")]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            return Ok(await _blogService.GetAllByCategoryIdAsync(id));
        }

        [HttpGet("{id}/[action]")]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> GetCategories(int id)
        {
            return Ok(_mapper.Map<List<CategoryListDto>>(await _blogService.GetcategoriesAsync(id)));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastFive()
        {
            return Ok(_mapper.Map<List<BlogListDto>>(await _blogService.GetLastFiveBlogsAsync()));
        }

        [HttpGet("{id}/[action]")]
        public async Task<IActionResult> GetComments([FromRoute]int id, [FromQuery]int? parentCommentId)
        {
            return Ok(_mapper.Map<List<CommentListDto>>(await _commentService.GetAllWithSubCommentsAsync(id,parentCommentId)));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery]string s)
        {
            return Ok(_mapper.Map<List<BlogListDto>>(await _blogService.SearchAsync(s)));
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> AddComment(CommentAddDto commentAddDto)
        {
            commentAddDto.PostedTime = DateTime.Now;
            await _commentService.AddAsync(_mapper.Map<Comment>(commentAddDto));
            return Created("", commentAddDto);
        }
    }
}
