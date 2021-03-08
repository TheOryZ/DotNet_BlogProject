using BlogProject.DTO.Dtos.CommentDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CommentAddValidator : AbstractValidator<CommentAddDto>
    {
        public CommentAddValidator()
        {
            RuleFor(I => I.AuthorName).NotEmpty().WithMessage("Name field cannot be blank");
            RuleFor(I => I.AuthorEmail).NotEmpty().WithMessage("Email field cannot be blank");
            RuleFor(I => I.Description).NotEmpty().WithMessage("Description field cannot be blank");
        }
    }
}
