#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Andrey.Data;
using Andrey.Models;
using Microsoft.EntityFrameworkCore;

namespace Andrey.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public CreateModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }
        public IList<Teacher> teachers;
        public IActionResult OnGet()
        {
            teachers = _context.Teacher.Include(c => c.User).ToList(); ;
            foreach(Teacher t in teachers)
            {
                t.User = _context.User.First(u => u.UserID == t.UserID);
            }

            ViewData["TeacherID"] = new SelectList(teachers, "TeacherID", "User.FullName");
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
