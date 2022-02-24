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
    public class DeleteModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DeleteModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Lessong Lessong { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lessong = await _context.Lessong
                .Include(l => l.Course).FirstOrDefaultAsync(m => m.LessongID == id);

            if (Lessong == null)
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

            Lessong = await _context.Lessong.FindAsync(id);

            if (Lessong != null)
            {
                _context.Lessong.Remove(Lessong);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Courses");
        }
    }
}
