using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comments;
public class CreateCommentDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Title must contain at least 2 characters")]
    [MaxLength(30, ErrorMessage = "Title cannot be over 30 characters")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(2, ErrorMessage = "Content must contain at least 2 characters")]
    [MaxLength(512, ErrorMessage = "Content cannot be over 512 characters")]
    public string Content { get; set; } = string.Empty;
}
