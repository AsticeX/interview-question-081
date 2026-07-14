using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Common.Models;
using Application.Posts.Queries.GetPostById;
using Application.Posts.Queries.GetPosts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly ISender _sender;

    public PostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<GetPostsResult>>>> Get([FromQuery] GetPostsQuery query)
    {
        return Ok(await _sender.Send(query));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<GetPostByIdResult>>> GetById(int id)
    {
        var response = await _sender.Send(new GetPostByIdQuery(id));

        return response.Succeeded ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<List<CreatePostResult>>>> Create([FromBody] CreatePostCommand command)
    {
        var response = await _sender.Send(command);
        var created = response.Data!.First();

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeletePostCommand(id));
        if (result.Succeeded)
        {
            return NoContent();
        }

        return result.Message == "Post was not found." ? NotFound(result) : BadRequest(result);
    }
}
