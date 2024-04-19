using api.Data;
using api.Dtos.Post;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;
public class PostRepository(ApplicationDbContext db) : IPostRepository
{
    public async Task<Post> CreateAsync(CreatePostDto post)
    {
        Console.WriteLine("TAGIDS: ", post.TagIds);

        var tags = await db.Tags.Where(x => post.TagIds.Contains(x.Id)).ToListAsync();

        if (tags is null)
        {
            Console.WriteLine("tag je nulk kme plaky tuggy");
        }

        Console.WriteLine(string.Join(",", tags!.Select(x => x.Name)));

        Console.WriteLine("TAGS: ", tags);
        Console.WriteLine("TAGIDS: ", post);


        var createdPost = await db.Posts.AddAsync(post.FromCreatePostDto(tags!));
        await db.SaveChangesAsync();

        return createdPost.Entity;
    }

}
