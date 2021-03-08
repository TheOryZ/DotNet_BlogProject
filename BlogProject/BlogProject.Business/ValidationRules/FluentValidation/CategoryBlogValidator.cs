using BlogProject.DTO.Dtos.CategoryBlogDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryBlogValidator : AbstractValidator<CategoryBlogDto>
    {
        public CategoryBlogValidator()
        {
            RuleFor(I => I.CategoryId).InclusiveBetween(0, int.MaxValue).WithMessage("Category Id field cannot be blank");
            RuleFor(I => I.CategoryId).InclusiveBetween(0, int.MaxValue).WithMessage("Blog Id field cannot be blank");
        }
    }
}
