using Hairdresser.Api.Contracts.V1;
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
        private readonly IPostService _postService;
        public TagsController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Roles = "Admin")]
        //[Authorize(Policy = "TagViewer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAllTagsAsync());
        }


        [HttpGet(ApiRoutes.Tags.test)]
        public async Task<IActionResult> CheckUser()
        {
            var user1 = HttpContext.User;
            var userRoles = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var user = HttpContext.User.Identities.ToList();
            
            var isAud = HttpContext.User.Identity.IsAuthenticated;
            return Ok(user1.Claims);
        }
    }
}
