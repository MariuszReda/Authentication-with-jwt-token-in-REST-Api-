using Hairdresser.Api.Domain;
using Hairdresser.Api.Models;

namespace Hairdresser.Api.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(Login login);
        Task<AuthenticationResult> RegisterAsync(Register register);
        Task<AuthenticationResult> RegisterAdminAsync(Register register);
    }
}
