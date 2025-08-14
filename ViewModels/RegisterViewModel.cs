using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(40,MinimumLength = 8 ,ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword",ErrorMessage = "Password does not match")]

    public string Password { get; set; }

    [Required(ErrorMessage = "Confirmation password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
}

