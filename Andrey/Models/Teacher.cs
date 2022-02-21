using System.ComponentModel.DataAnnotations;

namespace Andrey.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        public int UserID { get; set; }
        public string? Description { get; set; }
        public User? User { get; set; }
        public ICollection<Course>? Courses { get; set; }

    }
}
