using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Comments.Commands.DeleteComment;

public sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response<bool>>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Response<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.FindByIdAsync(request.Id, cancellationToken);
        if (comment is null)
        {
            return Response<bool>.Failure("Comment was not found.");
        }

        _commentRepository.Remove(comment);
        await _commentRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true);
    }
}
