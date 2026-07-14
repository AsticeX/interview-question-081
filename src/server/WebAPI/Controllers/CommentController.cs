using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Common.Models;
using Application.Comments.Queries.GetCommentById;
using Application.Comments.Queries.GetComments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/comment")]
public class CommentController : ControllerBase
{
    private readonly ISender _sender;

    public CommentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [HttpGet("get")]
    public async Task<ActionResult<Response<List<GetCommentsResult>>>> Get([FromQuery] GetCommentsQuery query)
    {
        return Ok(await _sender.Send(query));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<GetCommentByIdResult>>> GetById(int id)
    {
        var response = await _sender.Send(new GetCommentByIdQuery(id));

        return response.Succeeded ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    [HttpPost("create")]
    public async Task<ActionResult<Response<List<CreateCommentResult>>>> Create([FromBody] CreateCommentCommand command)
    {
        var result = await _sender.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        var created = result.Data!.First();
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _sender.Send(new DeleteCommentCommand(id));
        return response.Succeeded ? NoContent() : NotFound(response);
    }
}
