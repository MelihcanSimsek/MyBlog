using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels;

public class CommentPostViewModel
{
    public Guid ParentId { get; set; }
    public string Name { get; set; } = "This is a comment message";

    [Required(ErrorMessage = "Comment Message is required")]
    [MinLength(length: 20, ErrorMessage = "Blog description must be greater than 20 characters long")]
    [Display(Name = "Message")]
    public string Description { get; set; }
    public string ImageUrl { get; set; } = "";
    public string Tags { get; set; } = "Comment, Argue";
}
