using Hairdresser.Api.Contracts.Requests;
using Hairdresser.Api.Contracts.V1;
using Hairdresser.Api.Domain;
using Hairdresser.Api.Extensions;
using Hairdresser.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hairdresser.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Roles = "Admin,User"
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Policy = "TagViewer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tagService.GetAllTagsAsync());
        }

        [HttpGet(ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute] string tagName)
        {
            var tag = await _tagService.GetTagByNameAsync(tagName);
            if(tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPost(ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var tag = new Tag
            {
                Name = request.Name,
                CreatorId = Guid.Parse(HttpContext.GetUserId()),
                CreatedOn = DateTime.UtcNow
            };

            var created = await _tagService.CreateTagAsync(tag);
            if (!created)
            {
                return BadRequest();
            }
           // var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var baseUrl = $"{HttpContext.Request.Scheme}//{HttpContext.Request.Path}";
            var location = baseUrl + "/" + ApiRoutes.Tags.Get.Replace("{tagName}", tag.Name);
            
            return Created(location, tag);
        }
        
        [HttpDelete(ApiRoutes.Tags.Delete)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] string tagName)
        {
            var deleted = await _tagService.DeleteTagAsync(tagName);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
