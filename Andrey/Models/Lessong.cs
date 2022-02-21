using System.ComponentModel.DataAnnotations;

namespace Andrey.Models
{
    public class Lessong
    {
        [Key]
        public int LessongID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public int CourseID { get; set; }
        public Course? Course { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public ICollection<Comment>? Comments { get; set; }


    }
}
