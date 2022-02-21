using System.ComponentModel.DataAnnotations;

namespace Andrey.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
        public ICollection<Sertificate>? Sertificates { get; set; }
    }
}
