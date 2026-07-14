using Application.Common.Models;
using MediatR;

namespace Application.Comments.Queries.GetComments;

public class GetCommentsQuery : IRequest<Response<List<GetCommentsResult>>>
{
    public int? PostId { get; set; }
}
