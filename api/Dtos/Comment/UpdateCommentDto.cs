using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment;
public class UpdateCommentDto
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