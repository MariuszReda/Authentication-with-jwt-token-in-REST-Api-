using Hairdresser.Api.Contracts.Requests;
using Hairdresser.Api.Contracts.Responses;
using Hairdresser.Api.Contracts.V1;
using Hairdresser.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hairdresser.Api.Controllers.V1
{
    public class IdentityController : Controller
    {

       // private readonly UserManager<Account> _userManager;

        IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService= identityService;
        }

        [HttpPost(ApiRoutes.Identity.RegisterAdmin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse 
                { 
                    Errors = ModelState.Values.SelectMany(x=>x.Errors.Select(mess=>mess.ErrorMessage))
                });
            }
            var authResponse = await _identityService.RegisterAdminAsync(request.Register);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.ErrorMessage
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });

        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(mess => mess.ErrorMessage))
                });
            };

            var authResponse = await _identityService.RegisterAsync(request.Register);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.ErrorMessage
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {

            var authResponse = await _identityService.LoginAsync(request.Login);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.ErrorMessage
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
    }
}
