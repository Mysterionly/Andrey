#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Andrey.Data;
using Andrey.Models;

namespace Andrey.Pages.Lessongs
{
    public class EditModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public EditModel(Andrey.Data.AndreyContext context)
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
           ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Lessong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessongExists(Lessong.LessongID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("/Courses/Index");
        }

        private bool LessongExists(int id)
        {
            return _context.Lessong.Any(e => e.LessongID == id);
        }
    }
}
