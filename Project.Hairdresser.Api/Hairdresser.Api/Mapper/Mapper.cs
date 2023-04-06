using Hairdresser.Api.Contracts.Responses;
using Hairdresser.Api.Domain;

namespace Hairdresser.Api.Mapper
{
    public class Mapper : IMapper
    {
        public PostResponse PostResponseMap(Post post)
        {
            return new PostResponse
            {   
                Id= post.Id,
                Name = post.Name, 
                UserId = post.AccountId,
                Tags = post.Tags.Select(x=> new TagResponse { Name = x.Name } )
            };
        }
    }
}
