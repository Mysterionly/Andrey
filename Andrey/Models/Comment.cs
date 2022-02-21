using System.ComponentModel.DataAnnotations;

namespace Andrey.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public int LessongID { get; set; }
        [Required]
        public string Content { get; set; }

        public User? User { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime Created { get; set; }

    }
}
