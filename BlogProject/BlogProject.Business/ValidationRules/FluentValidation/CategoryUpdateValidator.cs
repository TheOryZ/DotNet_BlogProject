using BlogProject.DTO.Dtos.CategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(I => I.Id).InclusiveBetween(0, int.MaxValue).WithMessage("Id field cannot be blank");
            RuleFor(I => I.Name).NotEmpty().WithMessage("Name field cannot be blank");
        }
    }
}
