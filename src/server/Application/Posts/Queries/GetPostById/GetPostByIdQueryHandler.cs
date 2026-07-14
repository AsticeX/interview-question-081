using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Posts.Queries.GetPostById;

public sealed class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Response<GetPostByIdResult>>
{
    private readonly IPostRepository _postRepository;

    public GetPostByIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response<GetPostByIdResult>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id, cancellationToken);
        return post is null
            ? Response<GetPostByIdResult>.Failure("Post was not found.")
            : Response<GetPostByIdResult>.Success(post);
    }
}
