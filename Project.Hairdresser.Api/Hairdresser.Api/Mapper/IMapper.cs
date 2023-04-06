using Hairdresser.Api.Contracts.Responses;
using Hairdresser.Api.Domain;

namespace Hairdresser.Api.Mapper
{
    public interface IMapper
    {
        PostResponse PostResponseMap(Post post);
    }
}
