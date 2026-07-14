using Application.Common.Models;
using MediatR;

namespace Application.Posts.Queries.GetPosts;

public class GetPostsQuery : IRequest<Response<List<GetPostsResult>>>
{
}
