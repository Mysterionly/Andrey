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

namespace Andrey.Pages.Comments
{
    public class DetailsModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DetailsModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment = await _context.Comment
                .Include(c => c.User).FirstOrDefaultAsync(m => m.CommentID == id);

            if (Comment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
