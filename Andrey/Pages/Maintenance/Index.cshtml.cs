﻿#nullable disable
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
    public class IndexModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public IndexModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public IList<AppMaintenance> AppMaintenance { get;set; }

        public async Task OnGetAsync()
        {
            AppMaintenance = await _context.AppMaintenances.ToListAsync();
        }
    }
}
