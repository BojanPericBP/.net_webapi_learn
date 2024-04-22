using api.Dtos.Post;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/posts")]
[ApiController]
public class PostController(IPostRepository postRepo) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePostDto postInput)
    {

        var retPost = (await postRepo.CreateAsync(postInput)).ToPostDto();

        // var result = CreatedAtActionResult(retPost,{Id = new {id}})
        return Ok(retPost);
    }
}
