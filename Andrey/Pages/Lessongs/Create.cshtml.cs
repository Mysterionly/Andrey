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

namespace Andrey.Pages.Lessongs
{
    public class CreateModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public CreateModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "Name");
            return Page();
        }

        [BindProperty]
        public Lessong Lessong { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Lessong.Add(Lessong);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
