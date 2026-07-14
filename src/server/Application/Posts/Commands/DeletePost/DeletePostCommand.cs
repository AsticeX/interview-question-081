using Application.Common.Models;
using MediatR;

namespace Application.Posts.Commands.DeletePost;

public class DeletePostCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }

    public DeletePostCommand()
    {
    }

    public DeletePostCommand(int id)
    {
        Id = id;
    }
}
