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

namespace Andrey.Pages.Sertificates
{
    public class EditModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public EditModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sertificate Sertificate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sertificate = await _context.Sertificate
                .Include(s => s.Course)
                .Include(s => s.Student).FirstOrDefaultAsync(m => m.SertificateID == id);
            Sertificate.Student.User = _context.User.FirstOrDefault(u => u.UserID == Sertificate.Student.UserID);

            if (Sertificate == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "Name");
            foreach(Student s in _context.Student)
            {
                s.User = _context.User.FirstOrDefault(u => u.UserID == s.UserID);
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "User.FullName");
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

            _context.Attach(Sertificate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SertificateExists(Sertificate.SertificateID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SertificateExists(int id)
        {
            return _context.Sertificate.Any(e => e.SertificateID == id);
        }
    }
}
