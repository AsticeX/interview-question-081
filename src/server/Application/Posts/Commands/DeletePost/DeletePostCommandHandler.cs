using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Posts.Commands.DeletePost;

public sealed class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Response<bool>>
{
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.FindByIdAsync(request.Id, cancellationToken);
        if (post is null)
        {
            return Response<bool>.Failure("Post was not found.");
        }

        var hasComments = await _postRepository.HasCommentsAsync(request.Id, cancellationToken);
        if (hasComments)
        {
            return Response<bool>.Failure("Cannot delete a post that still has comments.");
        }

        _postRepository.Remove(post);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true);
    }
}
