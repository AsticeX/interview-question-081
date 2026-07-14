namespace Application.Comments.Queries.GetComments;

public class GetCommentsResult
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public string CommentBy { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
