using Hairdresser.Api.Domain;

namespace Hairdresser.Api.Services
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllTagsAsync();
        Task<bool> DeleteTagAsync(string tagName);
        Task<bool> CreateTagAsync(Tag tag);
        Task<Tag> GetTagByNameAsync(string tagName);
    }
}
