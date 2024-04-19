

namespace api.Dtos.Post
{
    public class CreatePostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IEnumerable<int> TagIds { get; set; } = [];
    }
}