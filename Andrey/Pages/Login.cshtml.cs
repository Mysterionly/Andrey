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

namespace Andrey.Pages
{
    public class LoginModel : PageModel
    {
        //[BindProperty]
        //public string Username { get; set; }

        //[BindProperty]
        //public string Password { get; set; }

        private readonly Andrey.Data.AndreyContext _context;

        public LoginModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserData uData { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            
            var cookieLogin = Request.Cookies["AndreyRazorAppLogin"];
            var cookiePassword = Request.Cookies["AndreyRazorAppPassword"];
            uData = new UserData();
            uData.User = await _context.User.FirstOrDefaultAsync(u => u.Login == cookieLogin);

            if (uData.User == null || uData.User.Password != cookiePassword)
            {
                return Page();
            }
            return RedirectToPage("/Profile/Details");//, new { id = found.UserID });
        }
        public async Task<IActionResult> OnPostAsync()
        {
            User found = await _context.User.FirstOrDefaultAsync(u => u.Login == uData.User.Login);
            if (found == null || found.Password != uData.User.Password)
            {
                return NotFound();
            }
            uData.UId = found.UserID;
            uData.User = found;
            Response.Cookies.Append("AndreyRazorAppLogin", found.Login, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            });
            Response.Cookies.Append("AndreyRazorAppPassword", found.Password, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            });
            return RedirectToPage("/Profile/Details");//, new { id = found.UserID });
        }
    }
}
