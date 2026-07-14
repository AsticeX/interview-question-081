using Application.Posts.Queries.GetPostById;
using Application.Posts.Queries.GetPosts;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IPostRepository
{
    Task<List<GetPostsResult>> GetAsync(CancellationToken cancellationToken);

    Task<GetPostByIdResult?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);

    Task<bool> HasCommentsAsync(int id, CancellationToken cancellationToken);

    Task<Post?> FindByIdAsync(int id, CancellationToken cancellationToken);

    void Add(Post post);

    void Remove(Post post);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
