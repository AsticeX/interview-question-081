namespace Application.Posts.Commands.CreatePost;

public class CreatePostResult
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CommentCount { get; set; }
}
