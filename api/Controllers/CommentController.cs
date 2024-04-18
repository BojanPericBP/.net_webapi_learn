using api.Dtos.Comment;
using api.Dtos.Comments;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;

namespace api.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController(ICommentRepository commentRepo, IStockRepository stockRepo) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var comments = await commentRepo.GetAllAsync();

        var commentsModel = comments.Select(x => x.ToCommentDto());

        return Ok(commentsModel);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var comment = await commentRepo.GetByIdAsync(id);

        if (comment is null)
            return NotFound();

        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> CreateAsync([FromRoute] int stockId, [FromBody] CreateCommentDto commentInput)
    {
        var stock = await stockRepo.GetByIdAsync(stockId);

        if (stock is null)
            return NotFound();

        var comment = commentInput.ToCommentFromCreateDto(stockId);
        await commentRepo.CreateAsync(comment);

        return CreatedAtAction("GetById", new { id = comment.Id }, comment.ToCommentDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateCommentDto commentInput)
    {
        var comment = await commentRepo.UdpateAsync(id, commentInput.ToCommentFromUpdateDto());

        if (comment is null)
            return NotFound("Comment not found");

        return Ok(comment.ToCommentDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var item = await commentRepo.DeleteAsync(id);

        if (item is null)
            return NotFound("Comment not found");

        return Ok(item);
    }
}