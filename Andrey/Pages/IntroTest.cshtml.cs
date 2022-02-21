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
    public class IntroTestModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public IntroTestModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public AppMaintenance AppMaintenance { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AppMaintenance = await _context.AppMaintenances.FirstOrDefaultAsync(m => m.AttrID == "IntroTestPageContent");

            if (AppMaintenance == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
