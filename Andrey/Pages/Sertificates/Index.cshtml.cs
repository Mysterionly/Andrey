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
    public class IndexModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public IndexModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public IList<Sertificate> Sertificate { get;set; }

        public async Task OnGetAsync()
        {
            Sertificate = await _context.Sertificate
                .Include(s => s.Course)
                .Include(s => s.Student).ToListAsync();
            foreach (Sertificate s in Sertificate)
            {
                s.Student.User = _context.User.FirstOrDefault(u => u.UserID == s.Student.UserID);
            }
        }
    }
}
