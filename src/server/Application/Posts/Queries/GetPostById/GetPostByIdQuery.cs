using Application.Common.Models;
using MediatR;

namespace Application.Posts.Queries.GetPostById;

public class GetPostByIdQuery : IRequest<Response<GetPostByIdResult>>
{
    public int Id { get; set; }

    public GetPostByIdQuery()
    {
    }

    public GetPostByIdQuery(int id)
    {
        Id = id;
    }
}
