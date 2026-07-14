using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Posts.Queries.GetPosts;

public sealed class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, Response<List<GetPostsResult>>>
{
    private readonly IPostRepository _postRepository;

    public GetPostsQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response<List<GetPostsResult>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetAsync(cancellationToken);
        return Response<List<GetPostsResult>>.Success(posts);
    }
}
