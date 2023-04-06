using Hairdresser.Api.Domain;

namespace Hairdresser.Api.Contracts.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<TagResponse> Tags { get; set; }
    }
}
