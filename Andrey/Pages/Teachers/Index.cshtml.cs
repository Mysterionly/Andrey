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
    public class IndexModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public IndexModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public IList<Teacher> Teacher { get;set; }

        public async Task OnGetAsync()
        {
            Teacher = await _context.Teacher
                .Include(t => t.User).ToListAsync();

            //var cources = _context.Course;
            //foreach (Teacher t in Teacher)
            //{
            //    t.Courses = cources.Where(c => c.TeacherID == t.TeacherID).ToList();
            //}
            
        }
    }
}
