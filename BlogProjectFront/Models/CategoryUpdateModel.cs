using System.ComponentModel.DataAnnotations;

namespace BlogProjectFront.Models
{
    public class CategoryUpdateModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Name field is required.")]
        [Display(Name="Name")]
        public string Name { get; set; }
    }
}