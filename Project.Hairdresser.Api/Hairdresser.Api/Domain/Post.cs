using Hairdresser.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdresser.Api.Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }
        [NotMapped]
        public virtual List<PostTag> Tags { get; set; }
    }
}
