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

namespace Andrey.Pages.Maintenance
{
    public class DeleteModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DeleteModel(Andrey.Data.AndreyContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppMaintenance = await _context.AppMaintenances.FindAsync(id);

            if (AppMaintenance != null)
            {
                _context.AppMaintenances.Remove(AppMaintenance);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
