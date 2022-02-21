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

namespace Andrey.Pages.Sertificates
{
    public class DeleteModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DeleteModel(Andrey.Data.AndreyContext context)
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

            if (Sertificate == null)
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

            Sertificate = await _context.Sertificate.FindAsync(id);

            if (Sertificate != null)
            {
                _context.Sertificate.Remove(Sertificate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
