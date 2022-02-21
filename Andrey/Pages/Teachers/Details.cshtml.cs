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

namespace Andrey.Pages.Teachers
{
    public class DetailsModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DetailsModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public Teacher Teacher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Teacher = await _context.Teacher
                .Include(t => t.User).FirstOrDefaultAsync(m => m.TeacherID == id);

            if (Teacher == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
