using Microsoft.AspNetCore.Identity;

namespace Hairdresser.Api.Models
{
    public class Account : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;
        public ICollection<AccountRole> AccountRoles { get; set; } = null!;
        public string Name { get; set; } = "Jan";
        public string Surname { get; set; } = "Kowalski";
    }
}
