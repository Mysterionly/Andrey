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

namespace Andrey.Pages.Maintenance
{
    public class EditModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public EditModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppMaintenance AppMaintenance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppMaintenance = await _context.AppMaintenances.FirstOrDefaultAsync(m => m.ID == id);

            if (AppMaintenance == null)
            {
                return NotFound();
            }
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

            _context.Attach(AppMaintenance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppMaintenanceExists(AppMaintenance.ID))
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

        private bool AppMaintenanceExists(int id)
        {
            return _context.AppMaintenances.Any(e => e.ID == id);
        }
    }
}
