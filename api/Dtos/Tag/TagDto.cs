using api.Dtos.Post;

namespace api.Dtos.Tag;
public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<PostDto> Posts { get; set; } = [];
}
