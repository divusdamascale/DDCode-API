using DDCode.API.Data;
using DDCode.API.Models.Domain;
using DDCode.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDCode.API.Repositories.Implementation
{
    public class BlogPostRepository(ApplicationDbContext _context) : IBlogPostRepository
    {
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public Task DeleteAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public Task<BlogPost> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
