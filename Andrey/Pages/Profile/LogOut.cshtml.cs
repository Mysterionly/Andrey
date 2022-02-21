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

namespace Andrey.Pages.Profile
{
    public class LogOutModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public LogOutModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Response.Cookies.Delete("AndreyRazorAppLogin");
            Response.Cookies.Delete("AndreyRazorAppPassword");

            return RedirectToPage("/Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Response.Cookies.Delete("AndreyRazorAppLogin");
            Response.Cookies.Delete("AndreyRazorAppPassword");

            return RedirectToPage("/Index");
        }
    }
}
