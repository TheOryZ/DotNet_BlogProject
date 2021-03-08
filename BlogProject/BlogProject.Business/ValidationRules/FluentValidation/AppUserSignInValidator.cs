using BlogProject.DTO.Dtos.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class AppUserSignInValidator : AbstractValidator<AppUserSignInDto>
    {
        public AppUserSignInValidator()
        {
            RuleFor(I => I.UserName).NotEmpty().WithMessage("Username field cannot be blank");
            RuleFor(I => I.Password).NotEmpty().WithMessage("Password field cannot be blank");
        }
    }
}
