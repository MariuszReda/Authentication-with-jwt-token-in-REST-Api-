
using Hairdresser.Api.Domain;

namespace Hairdresser.Api.Contracts.Requests
{
    public class CreatePostRequest
    {
        public string Name { get; set; }
        public virtual List<PostTag> Tags { get; set; }
    }
}
