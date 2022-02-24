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

namespace Andrey.Pages.Sertificates
{
    public class CreateModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public CreateModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public IList<User> Users { get; set; }

        public IActionResult OnGet()
        {
            foreach (Student s in _context.Student)
            {
                s.User = _context.User.Find(s.UserID);
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "User.FullName");
            return Page();
        }

        [BindProperty]
        public Sertificate Sertificate { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sertificate.Add(Sertificate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
