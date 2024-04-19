

using api.Dtos.Tag;

namespace api.Dtos.Post;
public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public IEnumerable<TagDto> Tags { get; set; } = [];
}
