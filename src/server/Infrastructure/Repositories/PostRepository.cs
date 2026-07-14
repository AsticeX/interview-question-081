using Application.Common.Interfaces;
using Application.Comments.Queries.GetComments;
using Application.Posts.Queries.GetPostById;
using Application.Posts.Queries.GetPosts;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PostRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetPostsResult>> GetAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Posts
            .AsNoTracking()
            .OrderByDescending(post => post.CreatedAt)
            .Select(post => new GetPostsResult
            {
                Id = post.Id,
                Title = post.Title,
                ImageUrl = post.ImageUrl,
                CreatedBy = post.CreatedBy,
                CreatedAt = post.CreatedAt,
                CommentCount = post.Comments.Count
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GetPostByIdResult?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts
            .AsNoTracking()
            .Where(post => post.Id == id)
            .Select(post => new GetPostByIdResult
            {
                Id = post.Id,
                Title = post.Title,
                ImageUrl = post.ImageUrl,
                CreatedBy = post.CreatedBy,
                CreatedAt = post.CreatedAt,
                Comments = post.Comments
                    .OrderByDescending(comment => comment.CreatedAt)
                    .Select(comment => new GetCommentsResult
                    {
                        Id = comment.Id,
                        PostId = comment.PostId,
                        CommentBy = comment.CommentBy,
                        Message = comment.Message,
                        CreatedAt = comment.CreatedAt
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts.AnyAsync(post => post.Id == id, cancellationToken);
    }

    public async Task<bool> HasCommentsAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments.AnyAsync(comment => comment.PostId == id, cancellationToken);
    }

    public async Task<Post?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts.FindAsync([id], cancellationToken);
    }

    public void Add(Post post)
    {
        _dbContext.Posts.Add(post);
    }

    public void Remove(Post post)
    {
        _dbContext.Posts.Remove(post);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
