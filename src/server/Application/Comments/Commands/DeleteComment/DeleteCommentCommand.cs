using Application.Common.Models;
using MediatR;

namespace Application.Comments.Commands.DeleteComment;

public class DeleteCommentCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }

    public DeleteCommentCommand()
    {
    }

    public DeleteCommentCommand(int id)
    {
        Id = id;
    }
}
