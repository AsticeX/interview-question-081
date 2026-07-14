namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public string CommentBy { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public Post Post { get; set; } = null!;
}
