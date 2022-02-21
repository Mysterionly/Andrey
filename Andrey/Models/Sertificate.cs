using System.ComponentModel.DataAnnotations;

namespace Andrey.Models
{
    public class Sertificate
    {
        [Key]
        public int SertificateID { get; set; }

        public int StudentID { get; set; }

        public int CourseID { get; set; }

        public Student? Student { get; set; }
        public Course? Course { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Created { get; set; }
    }
}
