using DDCode.API.Models.Domain;

namespace DDCode.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid catgegoryId);
        Task<Category?> UpdateAsync(Category category);
    }
}
