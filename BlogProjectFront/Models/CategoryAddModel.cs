using System.ComponentModel.DataAnnotations;

namespace BlogProjectFront.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage="Name field is required.")]
        [Display(Name="Name")]
        public string Name { get; set; }
    }
}