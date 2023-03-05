using Hairdresser.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdresser.Api.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }       
        public DateTime CreatedOn { get; set; }
        public Guid CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public Account Account { get; set; }
    }
}
