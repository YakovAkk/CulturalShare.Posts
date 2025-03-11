using CulturalShare.PostWrite.Services.Services.Base;
using Microsoft.AspNetCore.Mvc;
using PostsWriteProto;

namespace CulturalShare.PostWrite.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostWriteService _postWriteService;

    public PostsController(IPostWriteService postWriteService)
    {
        _postWriteService = postWriteService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequest request)
    {
        var post = await _postWriteService.CreatePostAsync(request);
        return Ok(post);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePostAsync([FromBody] UpdatePostRequest request)
    {
        var post = await _postWriteService.UpdatePostAsync(request);
        return Ok(post);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeletePostAsync([FromRoute] int Id)
    {
        var request = new DeletePostRequest()
        {
            PostId = Id
        };

        var post = await _postWriteService.DeletePostAsync(request);
        return Ok(post);
    }
}
