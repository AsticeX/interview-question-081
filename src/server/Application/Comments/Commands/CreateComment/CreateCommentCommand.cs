using System.ComponentModel.DataAnnotations;
using Application.Common.Models;
using MediatR;

namespace Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Response<List<CreateCommentResult>>>
{
    [Required]
    public int PostId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CommentBy { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;
}
