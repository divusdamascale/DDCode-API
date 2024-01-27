using DDCode.API.Data;
using DDCode.API.Models.Domain;
using DDCode.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Category?> DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;

        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            _context.Entry(category).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
