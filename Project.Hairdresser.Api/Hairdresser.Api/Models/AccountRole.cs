using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace Hairdresser.Api.Models
{
    public class AccountRole : IdentityUserRole<Guid>
    {
        public Account Account { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
