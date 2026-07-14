using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Commands.CreatePost;

public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Response<List<CreatePostResult>>>
{
    private readonly IPostRepository _postRepository;

    public CreatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response<List<CreatePostResult>>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Title = request.Title,
            ImageUrl = request.ImageUrl,
            CreatedBy = request.CreatedBy
        };

        _postRepository.Add(post);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return Response<List<CreatePostResult>>.Success(
        [
            new CreatePostResult
            {
                Id = post.Id,
                Title = post.Title,
                ImageUrl = post.ImageUrl,
                CreatedBy = post.CreatedBy,
                CreatedAt = post.CreatedAt,
                CommentCount = 0
            }
        ]);
    }
}
