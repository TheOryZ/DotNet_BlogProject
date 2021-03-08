using BlogProject.DTO.Interfaces;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DTO.Dtos.CategoryDtos
{
    public class CategoryWithBlogsCountDto : IDto
    {
        public int BlogsCount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
