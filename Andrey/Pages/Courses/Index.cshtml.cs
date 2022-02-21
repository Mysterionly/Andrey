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

namespace Andrey.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public IndexModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }

        public async Task OnGetAsync()
        {
            Course = await _context.Course
                .Include(c => c.Teacher).ToListAsync();
            foreach (Course c in Course)
            {
                c.Teacher = _context.Teacher.First(t => t.TeacherID == c.TeacherID);
                c.Teacher.User = _context.User.First(u => u.UserID == c.Teacher.UserID);
            }
        }
    }
}
