using DDCode.API.Models.Domain;

namespace DDCode.API.Repositories.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetAsync(int id);
        Task UpdateAsync(BlogPost blogPost);
        Task DeleteAsync(BlogPost blogPost);
    }
}
