using Application.Comments.Queries.GetCommentById;
using Application.Comments.Queries.GetComments;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface ICommentRepository
{
    Task<List<GetCommentsResult>> GetAsync(int? postId, CancellationToken cancellationToken);

    Task<GetCommentByIdResult?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<Comment?> FindByIdAsync(int id, CancellationToken cancellationToken);

    void Add(Comment comment);

    void Remove(Comment comment);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
