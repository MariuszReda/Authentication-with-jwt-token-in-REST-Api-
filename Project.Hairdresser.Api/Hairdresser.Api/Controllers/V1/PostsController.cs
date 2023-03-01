using Hairdresser.Api.Contracts.Requests;
using Hairdresser.Api.Contracts.Responses;
using Hairdresser.Api.Contracts.V1;
using Hairdresser.Api.Domain;
using Hairdresser.Api.Extensions;
using Hairdresser.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hairdresser.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpGet(ApiRoutes.Post.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetPostsAsync());
        }


        [HttpPost(ApiRoutes.Post.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request) 
        {
            var post = new Post {
                Name = request.Name,
                AccountId = Guid.Parse( HttpContext.GetUserId())
            };

            if (post.Id != Guid.Empty)
            {
                post.Id = Guid.NewGuid();
            }
            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var location = baseUrl + "/" + ApiRoutes.Post.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };
            return Created(location, response);
        }


        [HttpGet(ApiRoutes.Post.Get)] 
        public async Task<IActionResult> Get([FromRoute]Guid postId) 
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if(post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }


        [HttpDelete(ApiRoutes.Post.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid postId) 
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You don't own this post" });
            }

            var delete = await _postService.DeletePostAsync(postId);
            if (delete)
                return NoContent();

            return NotFound();
        }


        [HttpPut(ApiRoutes.Post.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid postId, [FromBody] UpdatePostRequest request)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());
            if(!userOwnsPost)
            {
                return BadRequest(new { error = "You don't own this post" });
            }

            var post = await _postService.GetPostByIdAsync(postId);
            post.Name = request.Name;

            var update = await _postService.UpdatePostAsync(post); 
            if(update)
                return Ok();

            return NotFound();
        }

    }
}
