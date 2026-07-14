using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Comments.Queries.GetCommentById;

public sealed class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Response<GetCommentByIdResult>>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentByIdQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Response<GetCommentByIdResult>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.Id, cancellationToken);
        return comment is null
            ? Response<GetCommentByIdResult>.Failure("Comment was not found.")
            : Response<GetCommentByIdResult>.Success(comment);
    }
}
