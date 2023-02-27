using Hairdresser.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdresser.Api.Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Account User { get; set; }
    }
}
