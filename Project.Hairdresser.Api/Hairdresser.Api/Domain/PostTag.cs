using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdresser.Api.Domain
{
    public class PostTag
    {
        [ForeignKey(nameof(TagId))]
        public virtual Tag Tag { get; set; }
        public string Name { get; set; }
        public Guid TagId { get; set; }
        public virtual Post Post { get; set; }
        [ForeignKey(nameof(PostId))]
        public Guid PostId { get; set; }
    }
}
