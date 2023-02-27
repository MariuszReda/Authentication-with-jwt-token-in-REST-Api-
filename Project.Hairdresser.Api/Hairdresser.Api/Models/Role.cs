using Microsoft.AspNetCore.Identity;

namespace Hairdresser.Api.Models
{
    public class Role: IdentityRole<Guid>
    {
        public ICollection<AccountRole> AccountRoles { get; set; } = null!;
    }
}
