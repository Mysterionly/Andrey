#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Andrey.Data;
using Andrey.Models;

namespace Andrey.Pages.Lessongs
{
    public class DetailsModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DetailsModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Lessong Lessong { get; set; }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public Comment Comment { get; set; }
        public string cookieLogin;
        public string cookiePassword;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            cookieLogin = Request.Cookies["AndreyRazorAppLogin"];
            cookiePassword = Request.Cookies["AndreyRazorAppPassword"];

            User = _context.User.First(u => u.Login == cookieLogin);
            if (User.Password != cookiePassword)
            {
                User = null;
            }

            Lessong = await _context.Lessong
                .Include(l => l.Course).FirstOrDefaultAsync(m => m.LessongID == id);
            Lessong.Comments = _context.Comment.Where(c => c.LessongID == id).ToList();

            if (Lessong == null)
            {
                return NotFound();
            }
            foreach (Comment c in Lessong.Comments)
            {
                c.User = _context.User.First(u => u.UserID == c.UserID);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            cookieLogin = Request.Cookies["AndreyRazorAppLogin"];
            cookiePassword = Request.Cookies["AndreyRazorAppPassword"];

            User = _context.User.First(u => u.Login == cookieLogin);
            if (User.Password != cookiePassword)
            {
                User = null;
            }
            if (User != null)
            {
                Comment.UserID = User.UserID;

                if (id == null)
                {
                    return NotFound();
                }
                Comment.LessongID = (int)id;
                Comment.Created = DateTime.Now;

                _context.Comment.Add(Comment);
                await _context.SaveChangesAsync();
            }

            Lessong = await _context.Lessong.Include(l => l.Course).FirstOrDefaultAsync(m => m.LessongID == id);
            Lessong.Comments = _context.Comment.Where(c => c.LessongID == id).ToList();

            if (Lessong == null)
            {
                return NotFound();
            }
            foreach (Comment c in Lessong.Comments)
            {
                c.User = _context.User.First(u => u.UserID == c.UserID);
            }
            return Page();
        }
    }
}
