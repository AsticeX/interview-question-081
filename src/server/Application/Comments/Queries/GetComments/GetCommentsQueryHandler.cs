using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Comments.Queries.GetComments;

public sealed class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, Response<List<GetCommentsResult>>>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Response<List<GetCommentsResult>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetAsync(request.PostId, cancellationToken);
        return Response<List<GetCommentsResult>>.Success(comments);
    }
}
