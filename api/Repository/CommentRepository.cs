using System.Data.Common;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository(ApplicationDbContext _db) : ICommentRepository
{
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _db.Comments.ToListAsync();
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        var comment = await _db.Comments.FindAsync(id);

        if(comment is null)
            return null;

        return comment;
    }
}
