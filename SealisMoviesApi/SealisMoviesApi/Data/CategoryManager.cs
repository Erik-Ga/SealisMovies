using Microsoft.EntityFrameworkCore;
using SealisMoviesApi.Models;

namespace SealisMoviesApi.Data
{
    public class CategoryManager
    {
        private readonly MyDBContext _context;
        public CategoryManager(MyDBContext context) 
        {
            _context = context;
        }
        public async Task<List<Models.Category>> GetCategories()
        {


            List<Models.Category> categories = await _context.Category.ToListAsync();

            Console.WriteLine("Nu är " + categories.Count + " kategorier hämtade.");

            return categories;
        }
        public async Task AddCategory(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCategory(Category category)
        {
            _context.Category.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
