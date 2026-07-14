using Application.Common.Models;
using MediatR;

namespace Application.Comments.Queries.GetCommentById;

public class GetCommentByIdQuery : IRequest<Response<GetCommentByIdResult>>
{
    public int Id { get; set; }

    public GetCommentByIdQuery()
    {
    }

    public GetCommentByIdQuery(int id)
    {
        Id = id;
    }
}
