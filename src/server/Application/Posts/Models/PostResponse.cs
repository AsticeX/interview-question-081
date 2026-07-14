namespace Application.Posts.Models;

public sealed record PostResponse(
    int Id,
    string Title,
    string? ImageUrl,
    string? CreatedBy,
    DateTime CreatedAt,
    int CommentCount);
