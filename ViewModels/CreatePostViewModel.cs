using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels;

public class CreatePostViewModel
{
    [Required(ErrorMessage = "Blog title is required")]
    [Display(Name = "Title")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Blog description is required")]
    [MinLength(length:20,ErrorMessage = "Blog description must be greater than 20 characters long")]
    public string Description { get; set; }

    [Display(Name = "Image URL")]
    public string ImageUrl { get; set; }

    [Required(ErrorMessage = "Blog tags are required")]
    public string Tags { get; set; }
}


