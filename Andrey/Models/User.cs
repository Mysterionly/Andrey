using System.ComponentModel.DataAnnotations;

namespace Andrey.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Фамилия должна быть не короче 2-х символов и не длинее 30-и")]
        [Required]
        public string Surname { get; set; }

        [StringLength(30, MinimumLength = 2, ErrorMessage = "Имя должно быть не короче 2-х символов и не длинее 30-и")]
        [Required]
        public string Name { get; set; }

        [StringLength(30, MinimumLength = 2, ErrorMessage = "Отчество должно быть не короче 2-х символов и не длинее 30-и")]
        [Required]
        public string MidName { get; set; }

        [StringLength(10, MinimumLength = 5)]
        [Required]
        public string Login { get; set; }

        [StringLength(10, MinimumLength = 5)]
        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public Student? Students { get; set; }
        public Teacher? Teachers { get; set; }
        public ICollection<Comment>? Comments { get; set; }

        public string FullName
        {
            get
            {
                return Surname + " " + Name + " " + MidName;
            }
        }
    }
}
