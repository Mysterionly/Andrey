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

namespace Andrey.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DeleteModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }
        public IList<Sertificate> Sertificate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student
                .Include(s => s.User).FirstOrDefaultAsync(m => m.StudentID == id);

            if (Student == null)
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

            Student = await _context.Student.FindAsync(id);
            Sertificate = _context.Sertificate.Include(s => s.StudentID == id).ToList();

            if (Student != null)
            {
                _context.Student.Remove(Student);
                foreach (Sertificate s in Sertificate)
                {
                    _context.Sertificate.Remove(s);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
