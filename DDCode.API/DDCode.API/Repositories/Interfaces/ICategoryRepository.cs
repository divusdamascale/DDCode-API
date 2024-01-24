using DDCode.API.Models.Domain;

namespace DDCode.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
    }
}
