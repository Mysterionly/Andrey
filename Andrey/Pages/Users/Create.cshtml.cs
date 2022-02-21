#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Andrey.Data;
using Andrey.Models;

namespace Andrey.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public CreateModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }
        public UserData UserData { get; set; }
        public IActionResult OnGetAsync()
        {
            //!!!!!
            var cookieLogin = Request.Cookies["AndreyRazorAppLogin"];
            var cookiePassword = Request.Cookies["AndreyRazorAppPassword"];
            if (cookieLogin == null || cookiePassword == null)
            {
                return RedirectToPage("/Login");
            }
            UserData = new UserData();
            UserData.User = _context.User.FirstOrDefault(u => u.Login == cookieLogin);
            if (UserData.User == null || UserData.User.Password != cookiePassword || !UserData.User.IsAdmin)
            {
                UserData = null;
                return RedirectToPage("/NoAccessGranted");

            }
            //!!!
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
