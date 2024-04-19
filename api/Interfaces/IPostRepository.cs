using api.Dtos.Post;
using api.Models;

namespace api.Interfaces;

public interface IPostRepository
{
    // public Task<List<PostDto>> GetAllAsync();

    public Task<Post> CreateAsync(CreatePostDto post);
}
