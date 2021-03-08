using BlogProject.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DTO.Dtos.CommentDtos
{
    public class CommentListDto : IDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; }
        public int? ParentCommentId { get; set; }
        public List<CommentListDto> SubCumments { get; set; }
        public int BlogId { get; set; }
    }
}
