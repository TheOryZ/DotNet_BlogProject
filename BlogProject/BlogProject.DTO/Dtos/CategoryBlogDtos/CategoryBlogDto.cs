using BlogProject.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DTO.Dtos.CategoryBlogDtos
{
    public class CategoryBlogDto : IDto
    {
        public int CategoryId { get; set; }
        public int BlogId { get; set; }
    }
}
