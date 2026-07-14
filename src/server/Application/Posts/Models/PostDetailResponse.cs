using Application.Comments.Models;

namespace Application.Posts.Models;

public sealed record PostDetailResponse(
    int Id,
    string Title,
    string? ImageUrl,
    string? CreatedBy,
    DateTime CreatedAt,
    IReadOnlyCollection<CommentResponse> Comments);
