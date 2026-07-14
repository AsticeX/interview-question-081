using System.ComponentModel.DataAnnotations;
using Application.Common.Models;
using MediatR;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<Response<List<CreatePostResult>>>
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    [MaxLength(100)]
    public string? CreatedBy { get; set; }
}
