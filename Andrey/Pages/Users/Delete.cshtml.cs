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
using System.Web;

namespace Andrey.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DeleteModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }
        public UserData UserData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //!!!!!
            var cookieLogin = Request.Cookies["AndreyRazorAppLogin"];
            var cookiePassword = Request.Cookies["AndreyRazorAppPassword"];
            if (cookieLogin == null || cookiePassword == null)
            {
                return RedirectToPage("/NoAccessGranted");
            }
            UserData = new UserData();
            UserData.User = _context.User.FirstOrDefault(u => u.Login == cookieLogin);
            if (UserData == null || UserData.User.Password != cookiePassword || !UserData.User.IsAdmin)
            {
                return RedirectToPage("/NoAccessGranted");

            }
            //!!!

            User = await _context.User.FirstOrDefaultAsync(m => m.UserID == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.FindAsync(id);

            if (User != null)
            {
                _context.User.Remove(User);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
