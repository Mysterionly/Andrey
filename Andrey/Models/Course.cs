using System.ComponentModel.DataAnnotations;

namespace Andrey.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int TeacherID { get; set; }
        public Teacher? Teacher { get; set; }
        public ICollection<Lessong>? Lessongs { get; set; }
        public ICollection<Sertificate>? Sertificates { get; set; }

    }
}
