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

namespace Andrey.Pages
{
    public class FinishTestModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public FinishTestModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public AppMaintenance AppMaintenance { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AppMaintenance = await _context.AppMaintenances.FirstOrDefaultAsync(m => m.AttrID == "FinishTestPageContent");

            if (AppMaintenance == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
