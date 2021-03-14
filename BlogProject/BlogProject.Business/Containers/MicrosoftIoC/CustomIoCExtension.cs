using BlogProject.Business.Concrete;
using BlogProject.Business.Interfaces;
using BlogProject.Business.Tools.FacadeTool;
using BlogProject.Business.Tools.JWT;
using BlogProject.Business.Tools.LogTool;
using BlogProject.Business.ValidationRules.FluentValidation;
using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Context;
using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.Dtos.AppUserDtos;
using BlogProject.DTO.Dtos.CategoryBlogDtos;
using BlogProject.DTO.Dtos.CategoryDtos;
using BlogProject.DTO.Dtos.CommentDtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BlogProject.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<BlogContext>();

            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<IBlogDal, EfBlogRepository>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICommentDal, EfCommentRepository>();

            services.AddScoped<IJwtService, JwtManager>();
            services.AddScoped<ICustomLogger, NLogAdapter>();
            services.AddScoped<IFacade, Facade>();

            services.AddTransient<IValidator<AppUserSignInDto>, AppUserSignInValidator>();
            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddValidator>();
            services.AddTransient<IValidator<CategoryBlogDto>, CategoryBlogValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateValidator>();
            services.AddTransient<IValidator<CommentAddDto>, CommentAddValidator>();
        }
    }
}
