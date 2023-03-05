using Hairdresser.Api.Data;
using Hairdresser.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hairdresser.Api.Services
{
    public class TagService : ITagService
    {
        private readonly DataContext _dataContext;
        public TagService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateTagAsync(Tag tag)
        {
            await _dataContext.Tags.AddAsync(tag);
            var create = await _dataContext.SaveChangesAsync();
            return create > 0;
        }

        public async Task<bool> DeleteTagAsync(string tagName)
        {
            var exist = await _dataContext.Tags.Where(t => t.Name == tagName).FirstOrDefaultAsync();
            if (exist != null)
            {
                _dataContext.Tags.Remove(exist);
                _dataContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _dataContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagByNameAsync(string tagName)
        {
            return await _dataContext.Tags.Where(x => x.Name == tagName).FirstOrDefaultAsync();
        }
    }
}
