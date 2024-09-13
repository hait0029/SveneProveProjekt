namespace SvenePrøveProjekt.Interfaces
{
    public interface ICategoryRepo
    {
        public Task<List<Category>> GetCategory();
        public Task<Category> GetCategoryById(int categoryId);
        public Task<Category> CreateCategory(Category category);
        public Task<Category> UpdateCategory(int categoryId, Category category);
        public Task<Category> DeleteCategory(int categoryId);
    }
}
