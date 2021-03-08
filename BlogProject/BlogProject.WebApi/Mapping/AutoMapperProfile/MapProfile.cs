using AutoMapper;
using BlogProject.DTO.Dtos.AppUserDtos;
using BlogProject.DTO.Dtos.BlogDtos;
using BlogProject.DTO.Dtos.CategoryDtos;
using BlogProject.DTO.Dtos.CommentDtos;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.WebApi.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<BlogListDto, Blog>();
            CreateMap<Blog, BlogListDto>();
            CreateMap<BlogAddDto, Blog>();
            CreateMap<Blog, BlogAddDto>();
            CreateMap<BlogAddModel, Blog>();
            CreateMap<Blog, BlogAddModel>();
            CreateMap<BlogUpdateModel, Blog>();
            CreateMap<Blog, BlogUpdateModel>();

            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryAddDto>();
            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();

            CreateMap<AppUserSignInDto, AppUser>();
            CreateMap<AppUser, AppUserSignInDto>();

            CreateMap<Comment, CommentListDto>();
            CreateMap<CommentListDto, Comment>();
            CreateMap<Comment, CommentAddDto>();
            CreateMap<CommentAddDto, Comment>();
        }
    }
}
