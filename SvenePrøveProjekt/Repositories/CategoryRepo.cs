using Microsoft.EntityFrameworkCore;

namespace SvenePrøveProjekt.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private DatabaseContext _context { get; set; }
        public CategoryRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category newCategory)
        {
            _context.Category.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category> DeleteCategory(int categoryId)
        {
            Category category = await GetCategoryById(categoryId);
            if (category != null)
            {
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }
            return category;
        }

        public async Task<List<Category>> GetCategory()
        {
            return await _context.Category.Include(e => e.product).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            //return await _context.Category.FirstOrDefaultAsync(e => e.CategoryID == categoryId);
            return await _context.Category.Include(e => e.product).FirstOrDefaultAsync(e => e.CategoryID == categoryId);
        }

        public async Task<Category> UpdateCategory(int categoryId, Category updateCategory)
        {
            Category category = await GetCategoryById(categoryId);
            if (category != null && updateCategory != null)
            {
                category.CategoryName = updateCategory.CategoryName;

                await _context.SaveChangesAsync();
            }
            return category;
        }
    }
}

