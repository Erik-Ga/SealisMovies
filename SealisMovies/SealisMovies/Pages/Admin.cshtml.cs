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
        public List<Discussion> ReportedDiscussions { get; set; }

        [BindProperty]
        public Models.Category Category { get; set; }
        public async Task<IActionResult> OnGetAsync(int editid, int deleteid)
        {
            Categories = await DAL.CategoryManagerAPI.GetAllCategories();
            ReportedDiscussions = await _context.Discussions
                .Where(d => d.Reported == true)
                .ToListAsync();
            if (editid != 0)
            {
                Category = await DAL.CategoryManagerAPI.GetCategory(editid);
            }

            if (deleteid != 0)
            {
                await DAL.CategoryManagerAPI.DeleteCategory(deleteid);
                Categories = await DAL.CategoryManagerAPI.GetAllCategories();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateCategoryAsync()
        {
            if (ModelState.IsValid)
            {
                await DAL.CategoryManagerAPI.SaveCategory(Category);
            }
            Categories = await DAL.CategoryManagerAPI.GetAllCategories();
            return RedirectToPage("/Admin");
        }

        public async Task<IActionResult> OnPostRemoveCategoryAsync(int deleteid)
        {
            if (deleteid != 0)
            {
                await DAL.CategoryManagerAPI.DeleteCategory(deleteid);
                Categories = await DAL.CategoryManagerAPI.GetAllCategories();
            }

            return RedirectToPage("/Admin");
        }

        public async Task<IActionResult> OnPostUpdateCategoryAsync(int editid)
        {
            if (ModelState.IsValid)
            {
                Category = await DAL.CategoryManagerAPI.GetCategory(editid);
                await DAL.CategoryManagerAPI.SaveCategory(Category);
            }
            Categories = await DAL.CategoryManagerAPI.GetAllCategories();
            return RedirectToPage("/Admin");
        }
        public async Task<IActionResult> OnPostDeleteDiscussionAsync(int discussionId)
        {
            var discussion = await _context.Discussions.FindAsync(discussionId);
            if (discussion != null)
            {
                _context.Discussions.Remove(discussion);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Admin");
        }
        public async Task<IActionResult> OnPostRevertReportedStatusAsync(int discussionId)
        {
            var discussion = await _context.Discussions.FindAsync(discussionId);
            if (discussion != null)
            {
                discussion.Reported = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Admin");
        }

    }
}
