namespace Application.Comments.Models;

public sealed record CommentResponse(
    int Id,
    int PostId,
    string CommentBy,
    string Message,
    DateTime CreatedAt);
