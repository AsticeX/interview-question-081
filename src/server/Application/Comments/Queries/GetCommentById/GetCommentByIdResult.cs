namespace Application.Comments.Queries.GetCommentById;

public class GetCommentByIdResult
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public string CommentBy { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
