using api.Data;
using api.Dtos.Tag;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Repository;
public class TagRepository(ApplicationDbContext db) : ITagRepository
{
    public async Task<Tag> CreateAsync(Tag tag)
    {
        var createdTag = await db.Tags.AddAsync(tag);
        await db.SaveChangesAsync();
        return createdTag.Entity;
    }
}
