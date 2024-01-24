using DDCode.API.Data;
using DDCode.API.Models.Domain;
using DDCode.API.Repositories.Interfaces;

namespace DDCode.API.Repositories.Implementation
{
    public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
    {
        public readonly ApplicationDbContext _context = context;
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
