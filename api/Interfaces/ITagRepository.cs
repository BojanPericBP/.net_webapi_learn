using api.Dtos.Tag;
using api.Models;

namespace api.Interfaces;
public interface ITagRepository
{
    public Task<Tag> CreateAsync(Tag tagInput);
}
