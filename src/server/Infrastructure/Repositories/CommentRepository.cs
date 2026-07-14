using Application.Comments.Queries.GetCommentById;
using Application.Comments.Queries.GetComments;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetCommentsResult>> GetAsync(int? postId, CancellationToken cancellationToken)
    {
        var query = _dbContext.Comments
            .AsNoTracking()
            .AsQueryable();

        if (postId.HasValue)
        {
            query = query.Where(comment => comment.PostId == postId.Value);
        }

        return await query
            .OrderByDescending(comment => comment.CreatedAt)
            .Select(comment => new GetCommentsResult
            {
                Id = comment.Id,
                PostId = comment.PostId,
                CommentBy = comment.CommentBy,
                Message = comment.Message,
                CreatedAt = comment.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GetCommentByIdResult?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments
            .AsNoTracking()
            .Where(comment => comment.Id == id)
            .Select(comment => new GetCommentByIdResult
            {
                Id = comment.Id,
                PostId = comment.PostId,
                CommentBy = comment.CommentBy,
                Message = comment.Message,
                CreatedAt = comment.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Comment?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments.FindAsync([id], cancellationToken);
    }

    public void Add(Comment comment)
    {
        _dbContext.Comments.Add(comment);
    }

    public void Remove(Comment comment)
    {
        _dbContext.Comments.Remove(comment);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
