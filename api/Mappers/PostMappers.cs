using api.Dtos.Post;
using api.Models;

namespace api.Mappers
{
    public static class PostMappers
    {
        public static Post FromCreatePostDto(this CreatePostDto from, List<Tag> tags)
        {
            return new Post
            {
                Title = from.Title,
                Content = from.Content,
                Tags = tags
            };
        }

        public static PostDto ToPostDto(this Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Tags = post.Tags.Select(x => x.ToTagDto())
            };
        }
    }
}