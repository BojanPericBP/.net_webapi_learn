using System.Diagnostics;
using api.Dtos.Tag;
using api.Models;

namespace api.Mappers
{
    public static class TagMappers
    {
        public static TagDto ToTagDto(this Tag tag)
        {
            return new TagDto
            {
                Id = tag.Id,
                Description = tag.Description
            };
        }

        public static Tag FromCreateTagToTag(this CreateTagDto from)
        {
            return new Tag
            {
                Name = from.Name,
                Description = from.Description,
            };
        }
    }
}