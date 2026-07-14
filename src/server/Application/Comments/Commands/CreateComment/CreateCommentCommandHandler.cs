using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Comments.Commands.CreateComment;

public sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<List<CreateCommentResult>>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task<Response<List<CreateCommentResult>>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var postExists = await _postRepository.ExistsAsync(request.PostId, cancellationToken);
        if (!postExists)
        {
            return Response<List<CreateCommentResult>>.Failure($"PostId {request.PostId} does not exist.");
        }

        var comment = new Comment
        {
            PostId = request.PostId,
            CommentBy = request.CommentBy,
            Message = request.Message
        };

        _commentRepository.Add(comment);
        await _commentRepository.SaveChangesAsync(cancellationToken);

        return Response<List<CreateCommentResult>>.Success(
        [
            new CreateCommentResult
            {
                Id = comment.Id,
                PostId = comment.PostId,
                CommentBy = comment.CommentBy,
                Message = comment.Message,
                CreatedAt = comment.CreatedAt
            }
        ]);
    }
}
