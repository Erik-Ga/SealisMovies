using Microsoft.EntityFrameworkCore;
using SealisMoviesApi.Models;

namespace SealisMoviesApi.Data
{
    public class CategoryManager
    {
        public static List<Models.Category> Categories { get; set; }

        private readonly MyDBContext _context;
        public CategoryManager(MyDBContext context) 
        {
            _context = context;
        }
        public async Task<List<Models.Category>> GetAllCategories()
        {
            //if (Categories == null || !Categories.Any())
            //{
                Categories = await _context.Category.ToListAsync();
            //}

            return Categories;
        }
        public async Task<Models.Category> GetCategory(int id)
        {
            if (Categories == null || !Categories.Any())
            {
                Categories = await GetAllCategories();
            }


            var existingCate = Categories.Where(p => p.Id == id).SingleOrDefault();

            if (existingCate != null)
            {
                return existingCate;
            }
            else
            {
                return null;
            }
        }
        public async Task AddCategory(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateCategory(Category category, int id)
        {
            if (Categories is null)
            {
                await GetAllCategories();
            }

            if (category != null)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}
