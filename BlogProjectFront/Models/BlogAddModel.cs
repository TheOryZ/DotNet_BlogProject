using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogProjectFront.Models
{
    public class BlogAddModel
    {
        [Required(ErrorMessage="Title field is required.")]
        [Display(Name="Title")]
        public string Title { get; set; }
        [Required(ErrorMessage="Description field is required.")]
        [Display(Name="Description")]
        public string Description { get; set; }
        [Required(ErrorMessage="Short Description field is required.")]
        [Display(Name="Short Description")]
        public string ShortDescription { get; set; }
        [Display(Name="Select Picture :")]
        public IFormFile Image { get; set; }
        public int AppUserId { get; set; }
    }
}