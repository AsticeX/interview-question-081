using Application.Comments.Queries.GetComments;

namespace Application.Posts.Queries.GetPostById;

public class GetPostByIdResult
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<GetCommentsResult> Comments { get; set; } = [];
}
