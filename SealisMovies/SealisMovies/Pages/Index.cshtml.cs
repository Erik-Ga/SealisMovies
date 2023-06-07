﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SealisMovies.Data.Migrations;
using SealisMovies.Models;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace SealisMovies.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public List<ProfilePicture> ProfilePictures { get; set; }
        public List<Models.Discussion> Discussions { get; set; }
        public List<Models.Message> Messages { get; set; }
        public List<Models.Comment> Comments { get; set; }

        [BindProperty]
        public Models.Comment Comment { get; set; }

        [BindProperty]
        public Models.Discussion Discussion { get; set; }

        [BindProperty]
        public Models.Message Message { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int showid, int deleteid, string recieverid, string recievername, int reportid, int discussionid)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if(discussionid != null)
            {
                Comment = new Models.Comment();

                Comment.DiscussionId = discussionid;
            }
            if (recieverid != null)
            {
                Message = new Models.Message();

                Message.SenderName = currentUser.UserName;
                Message.ReceiverName = recievername;
                Message.RecieverId = recieverid;
            }
            Comments = await _context.Comment.ToListAsync();
            Messages = await _context.Message.ToListAsync();
            ProfilePictures = await _context.ProfilePicture.ToListAsync();
            if (reportid != 0)
            {
                return await OnPostReportAsync(reportid);
            }
            if (showid != 0)
            {
                Discussion = await _context.Discussions.FindAsync(showid);
            }

            Models.Discussion discussion = await _context.Discussions.FindAsync(deleteid);
            if (deleteid != 0)
            {
                if(discussion != null) 
                {
                    if (System.IO.File.Exists("./wwwroot/img/" + discussion.Image))
                    {
                        System.IO.File.Delete("./wwwroot/img/" + discussion.Image);
                    }

                    var comments = _context.Comment.Where(c => c.DiscussionId == discussion.Id);
                    _context.Comment.RemoveRange(comments);

                    _context.Discussions.Remove(discussion);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }
            }
            Discussions = await _context.Discussions.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string recieverid, string recievername, int reportid, int discussionid)
        {
            TempData["SuccessMessage"] = "Ditt meddelande har skickats!";

            if (reportid != 0)
            {
                return await OnPostReportAsync(reportid);
            }
            if (discussionid != 0)
            {
                return await OnPostCommentAsync(reportid);
            }
            var currentUser = await _userManager.GetUserAsync(User);

            Message.SenderId = currentUser.Id;
            Message.SenderName = currentUser.UserName;
            Message.RecieverId = recieverid;
            Message.ReceiverName = recievername;
            Message.Sent = true;
            

            _context.Add(Message);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostCommentAsync(int discussionid)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var profilePicture = await _context.ProfilePicture.FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            if (profilePicture != null)
            {
                Comment.Image = profilePicture.Image;
            }

            Comment.UserId = currentUser.Id;
            Comment.UserName = currentUser.UserName;
            Comment.DiscussionId = discussionid;
            Comment.Date = DateTime.Now;


            _context.Add(Comment);
            await _context.SaveChangesAsync();


            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostReportAsync(int reportid)
        {
            var discussion = await _context.Discussions.FindAsync(reportid);
            if (discussion != null)
            {
                discussion.Reported = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}