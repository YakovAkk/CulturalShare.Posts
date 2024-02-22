using CulturalShare.PostRead.Services.Services.Base;
using Microsoft.AspNetCore.Mvc;
using PostsReadProto;

namespace CulturalShare.PostRead.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostReadService _postService;

    public PostsController(IPostReadService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPostsAsync()
    {
        var posts = await _postService.GetPostsAsync();
        return Ok(posts);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetPostsAsync([FromRoute] int Id)
    {
        var request = new GetPostByIdRequest()
        {
            Id = Id,
        };
        var posts = await _postService.GetPostByIdAsync(request);
        return Ok(posts);
    }
}
