using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SealisMovies.Models;
using System.Reflection.Metadata;
using System.Security.Claims;


namespace SealisMovies.Pages
{


    public class AdminModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        public AdminModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Models.Category> Categories { get; set; }

        [BindProperty]
        public Models.Category Category { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateCategoryAsync()
        {
            if (!ModelState.IsValid)
            {
                // Handle invalid form data
                return Page();
            }
            // Add the category to the DbContext and save changes
            _context.Add(Category);
            await _context.SaveChangesAsync();

            Categories = await _context.Categories.ToListAsync();

            return RedirectToPage("/Admin");
        }

        public async Task<IActionResult> OnPostRemoveCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Admin");
        }

        public async Task<IActionResult> OnPostUpdateCategoryAsync(int categoryId, string newCategoryName)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                category.CategoryName = newCategoryName;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Admin");
        }

    }
}
