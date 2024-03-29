﻿using Hairdresser.Api.Data;
using Hairdresser.Api.Domain;
using Microsoft.EntityFrameworkCore;


namespace Hairdresser.Api.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;
        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Post>> GetPostsAsync() 
        { 
            return await _dataContext.Posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
           return await _dataContext.Posts.SingleOrDefaultAsync(x=>x.Id == postId);
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            await _dataContext.Posts.AddAsync(post);
            var create = await _dataContext.SaveChangesAsync();
            return create > 0;
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            var exists = await _dataContext.Posts.FirstOrDefaultAsync(x => x.Id == postToUpdate.Id);
            if (exists != null)
            {
                _dataContext.Posts.Update(exists);
                var update = await _dataContext.SaveChangesAsync();
                return update > 0;
            }
            return false;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var post = await GetPostByIdAsync(postId);
            if (post != null)
            {
                _dataContext.Posts.Remove(post);
                var deleted = await _dataContext.SaveChangesAsync();
                return deleted > 0;
            }
            return false;
        }

        public async Task<bool> UserOwnsPostAsync(Guid postId, string getUserId)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x=>x.Id == postId);

            if(post == null)
            {
                return false;
            }

            if(post.AccountId != Guid.Parse( getUserId))
            {
                return false;
            }

            return true;
        }


    }
}
