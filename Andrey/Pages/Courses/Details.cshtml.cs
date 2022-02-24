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
    public class DetailsModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DetailsModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }
        public User User { get; set; }
        public Teacher Teacher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course
                .Include(c => c.Teacher).FirstOrDefaultAsync(m => m.CourseID == id);
            Teacher = await _context.Teacher.FirstOrDefaultAsync(u => u.TeacherID == Course.TeacherID);
            User = await _context.User.FirstOrDefaultAsync(u => u.UserID == Teacher.UserID);


            if (Course == null)
            {
                return NotFound();
            }

            Course.Lessongs = _context.Lessong.Where(l => l.CourseID == id).ToList();

            return Page();
        }
    }
}
