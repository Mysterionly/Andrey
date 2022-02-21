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

namespace Andrey.Pages.Lessongs
{
    public class IndexModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public IndexModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public IList<Lessong> Lessong { get;set; }

        public async Task OnGetAsync()
        {
            Lessong = await _context.Lessong
                .Include(l => l.Course).ToListAsync();
        }
    }
}
