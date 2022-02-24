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

namespace Andrey.Pages.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DetailsModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public UserData UserData { get; set; }
        public List<Sertificate> Sertificates { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var cookieLogin = Request.Cookies["AndreyRazorAppLogin"];
            var cookiePassword = Request.Cookies["AndreyRazorAppPassword"];
            if (cookieLogin == null || cookiePassword == null)
            {
                return RedirectToPage("/Login");
            }

            UserData = new UserData();
            UserData.User = await _context.User.FirstOrDefaultAsync(u => u.Login == cookieLogin);

            if (UserData.User == null || UserData.User.Password != cookiePassword)
            {
                return RedirectToPage("/Login");
            }

            UserData.User.Teachers = await _context.Teacher.FirstOrDefaultAsync(u => u.UserID == UserData.User.UserID);
            UserData.User.Students = await _context.Student.FirstOrDefaultAsync(u => u.UserID == UserData.User.UserID);
            Sertificates = new List<Sertificate>();
            foreach (Sertificate s in _context.Sertificate)
            {
                if (s.StudentID == UserData.User.Students.StudentID)
                {
                    s.Course = _context.Course.FirstOrDefault(c => c.CourseID == s.CourseID);
                    Sertificates.Add(s);
                }
            }

            return Page();
        }
    }
}
