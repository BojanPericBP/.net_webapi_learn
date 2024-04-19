using api.Dtos.Tag;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/tags ")]
public class TagController(ITagRepository tagRepo) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTagDto tagInput)
    {
        var tagModel = tagInput.FromCreateTagToTag();

        var createdTag = await tagRepo.CreateAsync(tagModel);

        return Ok(createdTag.ToTagDto());
    }
}
