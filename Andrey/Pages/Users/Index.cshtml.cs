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

namespace Andrey.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public IndexModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }
        public UserData UserData { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //!!!!!
            var cookieLogin = Request.Cookies["AndreyRazorAppLogin"];
            var cookiePassword = Request.Cookies["AndreyRazorAppPassword"];
            if (cookieLogin == null || cookiePassword == null)
            {
                return RedirectToPage("/NoAccessGranted");
            }
            UserData = new UserData();
            UserData.User = await _context.User.FirstOrDefaultAsync(u => u.Login == cookieLogin);
            if (UserData.User == null || UserData.User.Password != cookiePassword || !UserData.User.IsAdmin)
            {
                return RedirectToPage("/NoAccessGranted");
            }

            //!!!!!
            else
            {
                User = await _context.User.ToListAsync();
            }
            return Page();
            
        }
    }
}
